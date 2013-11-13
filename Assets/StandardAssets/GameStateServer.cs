using UnityEngine;
#if UNITY_WEBPLAYER
#else
using System.Collections;
using System.Collections.Generic;
using System;
#endif

public class GameStateServer : MonoBehaviour 
{
	#if UNITY_WEBPLAYER
	
	#else

	static GameStateServer _instance;
	private static bool doneStart = false;
	public static System.Random rand = new System.Random();
	
	// use Network.connections to see all connections
	private List<NetworkPlayer> playersWaitingToGo = new List<NetworkPlayer>();
	internal Dictionary<NetworkPlayer,RunningGameData> dPlayerToGamedata = new Dictionary<NetworkPlayer,RunningGameData>();
	
	//database stuff
	// This is the file path of the database file we want to use
	// Right now, it'll load espDB.sqlite3 in the project's root folder.
	// If one doesn't exist, it will be automatically created.
	public static String databaseName = "db.sqlite3";
    private string[] RAY_TABLE_NAMES = new string[] { "player", "topscore", "confidenceLookupMC", "savedata", "game", "gamedetail" };
    private static int currentGameId = 1;

    internal DBManipulation dbManip = new DBManipulation( databaseName, false, false );
	internal ValidationStrategy valStrat = null; //initialized in Start() so that we can use the momoized (database) version intelligently/safely
	
	/// <summary>
	/// Static instance of the GameStateServer.
	///
	/// When you want to access the client without a direct
	/// reference (which you do in mose cases), use GameStateServer.Instance and the required
	/// GameObject initialization will be done for you.
	/// </summary>
	public static GameStateServer Instance {
		get {
		  if (_instance == null) {
		    _instance = FindObjectOfType(typeof(GameStateServer)) as GameStateServer;
		
		    if (_instance != null) {
		      return _instance;
		    }
		
		    GameObject client = new GameObject("__GameStateServer__");
		    _instance = client.AddComponent<GameStateServer>();
			_instance.Start();
			DontDestroyOnLoad( _instance );
			DebugConsole.Log( "GameStateServer instance created." );
		  }
		
		  return _instance;
		}
	}
	
	//not used? or is it auto called by MonoBehavior?
	public static void Init()
	{
		GameStateServer gss = Instance;
	}
	
	//returns null only in error
	//NOTE you may want to put your "decide which game to give the player" logic here (in the case that the dictionary doesn't have a mapping for this player -> game)
	private RunningGameData getRunningGameData( NetworkPlayer player, NetworkClient.MessType_ToServer messType, string args )
	{
		RunningGameData rgd = null;
		if( dPlayerToGamedata.ContainsKey( player ) )
		{//they are in the dictionary
			rgd = dPlayerToGamedata[ player ];
		}
		else// if( messType != NetworkClient.MessType_ToServer.PlayAgain && SINGLEPLAYER_ONLY )
		{//not in dictionary dPlayerToGamedata, and not a play again message; assume 1 player?
			DebugConsole.LogError( "non-existant player (not in dPlayerToGamedata). Assuming 1 player" );
			
			int tmpI = playersWaitingToGo.IndexOf( player );
		    if( tmpI >= 0 ) //disconnected from wait room.
			{
				//remove from wait room
				playersWaitingToGo.RemoveAt( tmpI ); 
				
				//make single player data
				RunningGameData_1player rgd1p = new RunningGameData_1player( this, /*-1*/ AssignNewGameID(), player );
                rgd1p.dPlayerData.Add(player, new PlayerData()); 
				rgd = rgd1p;
				this.dPlayerToGamedata.Add( player, rgd1p );

//					NetworkClient.Instance.SendClientMess( player, NetworkClient.MessType_ToClient.DomainDescription, ansPair.Value.ToString() );
//					NetworkClient.Instance.SendClientMess( player, NetworkClient.MessType_ToClient.PlayerPairReady );
			}else{//PLAY AGAIN?
				DebugConsole.LogError( "player was not in dPlayerToGamedata nor playersWaitingToGo, so we're ignoring them" );
//				//this player was not in the wait room, but also not in dPlayerToGamedata. odd?
//				return null;
			}
			
		}
		return rgd; //always returns null, unless we already have a mapping from player to RunningGameData
	}

    // HACK (kasiu): Not sure if this is the best place for it, but the server should be the one in charge.
    public int AssignNewGameID() {
        int gameId = currentGameId;
        currentGameId++;
        return gameId;
    }
	
	//THIS IS THE KEY METHOD OF THIS CLASS
	public void MessageFromClient( NetworkPlayer player, NetworkClient.MessType_ToServer messType, string args )
	{
		DebugConsole.LogWarning( "GameStateServer.MessageFromClient " + player + ": " + messType.ToString() );
		DebugConsole.Log( "args: " );
		if( args == null || args.Length == 0 )
			DebugConsole.Log( "<null or zero length>" );
		else
            DebugConsole.Log( args );
		
		//if( args != null && args.Length > 0 ) DebugConsole.Log( NetworkClient.RayToString( args ) );
		RunningGameData rgd = getRunningGameData( player, messType, args );		
		if( rgd == null )
			return; //this is ok.			
		
		DebugConsole.Log( "Hitting big message demux." );
		
		switch( messType )
		{
            //TODO case NetworkClient.MessType_ToServer.ReadDBStr:  "udid" arg
            case NetworkClient.MessType_ToServer.ReadDBStr:
                if( String.Compare( "udid", args, true ) == 0 )
                    NetworkClient.Instance.SendClientMess( player, NetworkClient.MessType_ToClient.UNTYPED, dbManip.getPlayerUDID( rgd.dPlayerData[ player ].playerid ) ); 
                break;
            case NetworkClient.MessType_ToServer.SaveDBStr:
                DebugConsole.Log("client requested save string : " + args);
                dbManip.dataDump(rgd.gameID, rgd.dPlayerData[player].playerid, args);
                DebugConsole.Log("dumped string info : " + rgd.gameID + ", " + rgd.dPlayerData[player].playerid + ", " + args);
                break;
		case NetworkClient.MessType_ToServer.DomainObjectIDsDeleted:
//			rgd.Mess_DomainObjectIDsDeleted( player, args );
//			rgd.SendFinalListAndFirstPairWhenReady( player );
			break;
			
		case NetworkClient.MessType_ToServer.DomainObjectIDsNOTDeleted:
//			rgd.Mess_DomainObjectIDsNOTDeleted( player, args );
//			rgd.SendFinalListAndFirstPairWhenReady( player );
			break;
			
		case NetworkClient.MessType_ToServer.DomainObjectNamesAdded:
//			rgd.Mess_DomainObjectNamesAdded( player, args );
//			rgd.SendFinalListAndFirstPairWhenReady( player );
			break;
			
		case NetworkClient.MessType_ToServer.JustGotToSwanScreen: //has playerScore arg; for when we're not doing add/remove objects screen
//			rgd.Mess_JustGotToSwanScreen( player, args );
			break;
			
		case NetworkClient.MessType_ToServer.SelectedRelation:
			//DebugConsole.Log( "MessageFromClient: MessType.SelectedRelation" );
//			rgd.Mess_SelectedRelation( player, args );
			break;
			
		case NetworkClient.MessType_ToServer.SwanAtEndOfScreen: //has playerScore, millisec args
//			rgd.Mess_SwanAtEndOfScreen( player, args );
			break;
			
		case NetworkClient.MessType_ToServer.PlayerHasNoLives: //has playerScore arg
//			rgd.Mess_PlayerHasNoLives( player, args );
			break;
			
		case NetworkClient.MessType_ToServer.PlayAgain: //HANDLED IN getRunningGameData
			//PlayerConnected( player );
//			NetworkClient.Instance.SendClientMess( player, NetworkClient.MessType_ToClient.RelationsList, strListRelations );
			
			//select, send the domain
//			System.Collections.Generic.KeyValuePair<int,System.Text.StringBuilder> ansPair = rgd.SelectDomainid();
//			if( ansPair.Equals( default(KeyValuePair<int,System.Text.StringBuilder>) ) )
//			{	//no more domains to work on! 
//				rgd.EndGame( "No more domains to play!", null );
//			}else{ 
//				rgd.ResetVarsForNewGame();
//				rgd.domainID = ansPair.Key;
//				NetworkClient.Instance.SendClientMess( player, NetworkClient.MessType_ToClient.DomainDescription, ansPair.Value.ToString() );
//				NetworkClient.Instance.SendClientMess( player, NetworkClient.MessType_ToClient.PlayerPairReady );
//			}
			break;
		case NetworkClient.MessType_ToServer.DeviceUniqueIdentifier:  //may be HANDLED IN getRunningGameData
			rgd.SetUniqueDeviceID( player, args ); //comes from NetworkClient.OnConnectedToServer
            DebugConsole.Log( "Got a UDID of: " + args );
			break;

        // UNIQUE AND SPECIFIC TO SNG ONLY
        // Whenever we want to create a new game, we do this.
        case NetworkClient.MessType_ToServer.SNGRequestNewGame:
            int gameId = dbManip.SaveGameBasics(dbManip.getPlayerUDID(rgd.dPlayerData[player].playerid));
            rgd.gameID = gameId;
            dbManip.SaveGameType(rgd.gameID, rgd.gameMode);

            List<string> objectSet = DBGWAPLoader.GenerateRandomObjectSet(7);
            List<string> tagSet = DBGWAPLoader.GenerateRandomTagset();
            rgd.objectSet = objectSet;
            rgd.tagSet = tagSet;

            // Sends the game mode
            NetworkClient.Instance.SendClientMess(player, NetworkClient.MessType_ToClient.SNGGameMode, rgd.gameMode.ToString());

            // Construct the object and tag sets.
            if (objectSet != null) {
                NetworkClient.Instance.SendClientMess(player, NetworkClient.MessType_ToClient.SNGObjectSet, DBStringHelper.listToString(objectSet, ','));
            }
            if (tagSet != null) {
                NetworkClient.Instance.SendClientMess(player, NetworkClient.MessType_ToClient.SNGTagSet, DBStringHelper.listToString(tagSet, ','));
            }

            int gamemode = dbManip.LookupPlayerGametype(rgd.dPlayerData[player].playerid);
            DebugConsole.Log("Game mode found = " + gamemode);
            if (gamemode == -1) {
                DebugConsole.Log("Need to add a new player.");
                dbManip.SavePlayerGameType(rgd.dPlayerData[player].playerid, rgd.gameMode);
            }

            break;

        case NetworkClient.MessType_ToServer.SNGRequestTrace:
            // (OH GOD BAD HARDCODED NUMBERS)
            string[] times = (dbManip.LookupRandomTraces(1, 1))[0].Split(',');
            DebugConsole.Log("Got a random timing thing from the database.");
            
            // Construct and send the partner trace.
            List<Triple<double, string, string>> partnerTrace = DBGWAPLoader.ConstructRandomPartnerTrace(times, rgd.objectSet, rgd.tagSet);
            if (partnerTrace != null) {
                NetworkClient.Instance.SendClientMess(player, NetworkClient.MessType_ToClient.SNGOpponentTrace, DBStringHelper.traceToString(partnerTrace, ':'));
                DebugConsole.Log("SENT A TRACE!");
            } else {
                DebugConsole.Log("DIDN'T SEND TRACE!");
            }
            break;

        case NetworkClient.MessType_ToServer.SNGSaveDBTrace:
            DebugConsole.Log("Got a trace from a player.");
            string[] traces = args.Split(',');
            // This is a really awful hack...someone just kill me please.
            // Basically, if I want to reuse all of my string helper functions, I have to manhandle some strings.
            // THIS IS WHY THE CLIENT SHOULDN'T AUTOSPLIT STRINGS WHEN READING FROM THE SERVER DAMMIT.
            // YOUR LOGIC IS NOT TWO-WAY...RANT...RANT...RAAAAAAAAAAAAAANT.
            for (int i = 0; i < traces.Length; i++) {
                traces[i] = traces[i].Replace(':', ',');
            }
            if (traces.Length != 3) {
                DebugConsole.Log("Ill-formed trace info.");
                break;
            }
            dbManip.SaveTraceResults(rgd.gameID, 1, traces[0]);
            dbManip.SaveTraceResults(rgd.gameID, 2, traces[1]);
            dbManip.SaveTraceResults(rgd.gameID, 3, traces[2]);
            string objSetStr = DBStringHelper.listToString(rgd.objectSet, ',');
            string tagSetStr = DBStringHelper.listToString(rgd.tagSet, ',');
            // XXX (kasiu): Need to save score properly
            dbManip.SaveGameGwapData(rgd.gameID, 0, rgd.dPlayerData[player].playerid, objSetStr, tagSetStr);
            // Increment player score:
            dbManip.IncrementPlayerGameCount(rgd.dPlayerData[player].playerid);
            break;

        case NetworkClient.MessType_ToServer.SNGSavePlayerData:
            // XXX (kasiu): Not used anymore.
            // used for SNG only
            int gametype = dbManip.LookupPlayerGametype(rgd.dPlayerData[player].playerid);
            if (gametype != -1) {
                DebugConsole.Log("Found an old player. Their gametype is : " + gametype);
                rgd.gameMode = gametype;
            } else {
                rgd.gameMode = UnityEngine.Random.Range(1, 4);
                DebugConsole.Log("Found a new player! Assigning a random gametype of " + rgd.gameMode);
                dbManip.SavePlayerGameType(rgd.dPlayerData[player].playerid, rgd.gameMode);
                //dbManip.SavePlayerInformation(dbManip.getPlayerUDID(rgd.dPlayerData[player].playerid), rgd.gameMode);
            }
            break;

        case NetworkClient.MessType_ToServer.SNGSavePlayerLikertData:
            DebugConsole.Log("Got some survey data! We'll be saving this!");
            string[] answers = args.Split(':');
            dbManip.SavePlayerLikertScores(rgd.dPlayerData[player].playerid, answers);
            break;
        
        case NetworkClient.MessType_ToServer.SNGSavePlayerScore:
            dbManip.SaveGameScore(rgd.gameID, System.Convert.ToInt32(args));
            break;

        default:
			DebugConsole.Log( "MessageFromClient: default" );
			break;
		}
	}
	
	//always sends relation list; handles wait room && pair up logic
	//autmotatically invoked
	public void PlayerConnected(NetworkPlayer player)
	{
		DebugConsole.LogWarning("Have a single player waiting to begin: player" + player);
		playersWaitingToGo.Add( player );
//			NetworkClient.Instance.SendClientMess( player, NetworkClient.MessType_ToClient.RelationsList, strListRelations );
	}
	
	public void PlayerDisconnected(NetworkPlayer player)
	{
		//clean up waiting pool
		int tmpI = playersWaitingToGo.IndexOf( player );

		if( tmpI >= 0 ) //disconnected from wait room.
			playersWaitingToGo.RemoveAt( tmpI );
		else
		{//disconnected from active game.
			//clean up active pool
			if( dPlayerToGamedata.ContainsKey( player ) )
			{
				RunningGameData rgd = dPlayerToGamedata[ player ];
				rgd.PlayerDisconnected( player );
			}else{
				Debug.LogWarning( "Player disconnected, however they were not in the waiting pool nor do I have running game data for them." );
			}
		}
		DebugConsole.LogWarning("Number players in waiting: " + playersWaitingToGo.Count );
	}
	
	//triggered when you close the server (any way you quit, besides kill -9)
	void OnApplicationQuit()
	{

        dbManip.OnApplicationQuit();
		while( playersWaitingToGo.Count > 0 )
		{
			NetworkPlayer player = playersWaitingToGo[ playersWaitingToGo.Count - 1 ];
			playersWaitingToGo.RemoveAt( playersWaitingToGo.Count - 1 );
			NetworkClient.Instance.SendClientMess( player, NetworkClient.MessType_ToClient.GameOver, "Server quit." );
		}
		
		NetworkPlayer[] tmpRay = new NetworkPlayer[ dPlayerToGamedata.Keys.Count ];
		dPlayerToGamedata.Keys.CopyTo( tmpRay, 0 );
		
		foreach( var key in tmpRay )
		{
			dPlayerToGamedata.Remove( key );
			NetworkClient.Instance.SendClientMess( key, NetworkClient.MessType_ToClient.GameOver, "Server quit." );
		}
	}
	
	
	//TODO we could make the list of tableNames to check for be an array, so that this method is a bit more "agnostic" to implementation
	void Start()
	{
		if( !doneStart )
		{//put init stuff here
			DebugConsole.Log("GameStateServer.Start()");
            
			dbManip.TryEnableForeignKeys(); //VERY IMPORTANT to ensure foreign keys are enforced
			
			if( dbManip.VerifyTableExistence( RAY_TABLE_NAMES ) ) 
			{
				//choose which Validation strategy you want to use
				valStrat = new ValidationStrategyMemoized( dbManip );
			}else{ //probably don't have a propperly set up db. 
				//TODO give them an easy way to set one up.
				DebugConsole.LogError( "Did not find expected table(s) in db. Make sure it is set up!" );
			}
			
			doneStart = true;
		}
	}	
#endif
	
}

-- create component type identification
insert into d_component values
(1,"moveToClick");

-- create component parameters for a given component
insert into component values
(1,1,"moveRate","10.0",current_timestamp),
(2,1,"delay","5.0",current_timestamp)
;

-- retrieve all components and their types
select * from d_component join component;


-- create dummy game
insert into game values
(1,"testDB",current_timestamp);

-- give dummy spec using first component
insert into game_spec values
(1,1,1,current_timestamp);

-- retrieve game spec along with components and their values
select * from game 
join game_spec
left join component
join d_component;

-- insert type of "action_timing" to know these are traces giving times for actions
insert into d_tracetype values
(1,"action_timing");

-- create dummy trace for first game
insert into gametrace values
(1,1,1,"3,5,10",current_timestamp);

-- retrieve dummy trace and it's type
select * from game
left join gametrace
join d_tracetype;


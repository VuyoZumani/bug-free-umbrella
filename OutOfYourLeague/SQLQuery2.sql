--update number of games played in league
UPDATE league 
SET P=played
FROM (
-- number of wins a team has
SELECT team,Count(*) AS played
FROM fixtures, league
WHERE  Team=teamonleft OR Team=teamonright
GROUP BY team) AS playedtable
WHERE playedtable.team=league.team;

--update number of wins in league
UPDATE league 
SET W=wins
FROM (
-- number of wins a team has
SELECT team,Count(*) AS wins
FROM fixtures, league
WHERE (scoreleft>scoreright AND Team=teamonleft) OR (scoreleft<scoreright AND Team=teamonright)
GROUP BY team) AS wintable
WHERE wintable.team=league.team;

--update number of losses in league
UPDATE league 
SET L=losses
FROM (
--number of losses a team has in fixtures
SELECT team,Count(*) AS losses
FROM fixtures, league
WHERE (scoreleft<scoreright AND Team=teamonleft) OR (scoreleft>scoreright AND Team=teamonright)
GROUP BY team) AS wintable
WHERE wintable.team=league.team;

--update number of draws in league
UPDATE league 
SET D=draws
FROM (
--number of draws a team has in fixtures
SELECT team,Count(*) AS draws
FROM fixtures, league
WHERE (scoreleft=scoreright AND team=teamonleft) OR (scoreleft=scoreright AND Team=teamonright)
GROUP BY team) AS wintable
WHERE wintable.team=league.team;

--update goals for each team
UPDATE league
SET GF=goalsfor
FROM (
-- get goal for each team
SELECT 
--to deal with cases when team is not present one side of the fixture
CASE WHEN team1 IS NULL THEN team2
			WHEN team2 IS NULL THEN team1
			ELSE team1 END AS team, 
--similarly deals with cases where there is no score on a side since a team is never on some side of the fixture
	   CASE WHEN totalonleft IS NULL THEN totalonright
			WHEN totalonright IS NULL THEN totalonleft
			ELSE totalonleft+totalonright END AS goalsfor
FROM (
-- returns a union of for the fixture
SELECT *
FROM (
-- gets the total goals scored when playing on the left side of fixture
SELECT team AS team1, SUM(scoreleft) AS totalonleft
FROM fixtures, league
WHERE team=teamonleft
GROUP BY team
) AS tableforgoalsonleft
FULL JOIN (
-- gets the total goals scored when on the right side of fixture
SELECT team AS team2, SUM(scoreright) AS totalonright
FROM fixtures, league
WHERE team=teamonright
GROUP BY team) AS tableforgoalsonright
ON tableforgoalsonleft.team1=tableforgoalsonright.team2) AS allinonetable) AS jointgftable
WHERE league.team=jointgftable.team;

--update goals against teams
UPDATE league
SET GA=goalsagainst
FROM (
-- get goal for a team
SELECT 
--to deal with cases when team is not present one side of the fixture
CASE WHEN team1 IS NULL THEN team2
			WHEN team2 IS NULL THEN team1
			ELSE team1 END AS team, 
--similarly deals with cases where there is no score on a side since a team is never on some side of the fixture
	   CASE WHEN totalonleft IS NULL THEN totalonright
			WHEN totalonright IS NULL THEN totalonleft
			ELSE totalonleft+totalonright END AS goalsagainst
FROM (
-- returns a union of for the fixture
SELECT *
FROM (
-- gets the total goals scored against when playing on the left side of fixture
SELECT team AS team1, SUM(scoreright) AS totalonleft
FROM fixtures, league
WHERE team=teamonleft
GROUP BY team
) AS tableforgoalsagainstonleft
FULL JOIN (
-- gets the total goals scored against when on the right side of fixture
SELECT team AS team2, SUM(scoreleft) AS totalonright
FROM fixtures, league
WHERE team=teamonright
GROUP BY team) AS tableforgoalsagainstonright
ON tableforgoalsagainstonleft.team1=tableforgoalsagainstonright.team2) AS allinonetable) AS jointgatable
WHERE league.team=jointgatable.team;


--update goal different for each team
UPDATE league
SET GD=goaldifference
FROM (
--gets the goal difference for each team
SELECT team, SUM(GF-GA) AS goaldifference
FROM league
GROUP BY team
) AS goaldifftable
WHERE league.team=goaldifftable.team;

--update points for each team
UPDATE league
SET Points=pts
FROM (
-- gets points for each team
SELECT team, SUM(3*W+D) AS pts
FROM league
GROUP BY team) AS pointstable
WHERE league.team=pointstable.team;


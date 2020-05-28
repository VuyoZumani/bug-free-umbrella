SELECT CASE WHEN team1.team>team2.team THEN team2.team ELSE team1.team END AS teamonLeft,
CASE WHEN team1.team>team2.team THEN team1.team ELSE team2.team END AS teamonRight
FROM rhodes AS team1 JOIN rhodes AS team2
ON 1=1
WHERE team1.team<>team2.team
GROUP BY
CASE WHEN team1.team>team2.team THEN team2.team
ELSE team1.team END,
CASE WHEN team1.team>team2.team THEN team1.team
ELSE team2.team END;
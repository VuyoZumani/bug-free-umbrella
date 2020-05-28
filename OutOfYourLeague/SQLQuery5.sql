SELECT * INTO features FROM (SELECT CASE WHEN team1.team>team2.team THEN team2.team 
ELSE team1.team END AS teamonLeft,0 AS scoreleft,0 AS scoreright, 
CASE WHEN team1.team>team2.team THEN team1.team
ELSE team2.team END AS teamonRight 
FROM dafbgh AS team1 JOIN dafbgh AS team2 
ON 1=1 
WHERE team1.team<>team2.team
GROUP BY 
CASE WHEN team1.team>team2.team THEN team2.team 
ELSE team1.team END, 
CASE WHEN team1.team>team2.team THEN team1.team 
ELSE team2.team END) AS features;
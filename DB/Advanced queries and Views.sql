SELECT 
    p.player_name,
    COUNT(m.match_id) AS total_winning_matches
	FROM ipl.Players p
	JOIN ipl.Matches m ON p.team_id = m.winner_team_id
	GROUP BY p.player_name
	ORDER BY total_winning_matches DESC
	LIMIT 1;

SELECT 
    m.venue,
    COUNT(m.match_id) AS total_matches,
    COUNT(fe.engagement_id) AS total_fan_engagements
	FROM ipl.Matches m
	JOIN ipl.Fan_Engagement fe ON m.match_id = fe.match_id
	GROUP BY m.venue
	ORDER BY total_matches DESC
	LIMIT 1;

SELECT 
    p.player_name,
    COUNT(fe.engagement_id) AS total_fan_engagements
	FROM ipl.Players p
	JOIN ipl.Matches m ON p.team_id = m.team1_id OR p.team_id = m.team2_id
	JOIN ipl.Fan_Engagement fe ON m.match_id = fe.match_id
	GROUP BY p.player_name
	ORDER BY total_fan_engagements DESC
	LIMIT 1;

SELECT 
    m.venue AS stadium_name,
    m.match_date,
    COUNT(fe.engagement_id) AS total_fan_engagements
	FROM ipl.Matches m
	JOIN ipl.Fan_Engagement fe ON m.match_id = fe.match_id
	GROUP BY m.venue, m.match_date
	ORDER BY total_fan_engagements DESC, m.match_date DESC
	LIMIT 1;

--views

CREATE VIEW ipl.TopPerformers AS
SELECT 
    p.player_name,
    t.team_name,
    p.matches_played
	FROM ipl.Players p
	JOIN ipl.Teams t ON p.team_id = t.team_id
	WHERE p.matches_played > 100;

SELECT * FROM ipl.TopPerformers;

CREATE VIEW ipl.MatchHighlights AS
SELECT 
    m.match_date,
    t1.team_name AS team1,
    t2.team_name AS team2,
    m.venue,
    wt.team_name AS winner
	FROM ipl.Matches m
	JOIN ipl.Teams t1 ON m.team1_id = t1.team_id
	JOIN ipl.Teams t2 ON m.team2_id = t2.team_id
	LEFT JOIN ipl.Teams wt ON m.winner_team_id = wt.team_id;

SELECT * FROM ipl.MatchHighlights;

CREATE VIEW ipl.FanEngagementStats AS
SELECT 
    m.match_date,
    m.venue,
    COUNT(fe.engagement_id) AS total_engagements
	FROM ipl.Matches m
	JOIN ipl.Fan_Engagement fe ON m.match_id = fe.match_id
	GROUP BY m.match_date, m.venue;

SELECT * FROM ipl.FanEngagementStats;

CREATE VIEW ipl.TeamPerformance AS
SELECT 
    t.team_name,
    COUNT(m.match_id) AS matches_played,
    SUM(CASE WHEN m.winner_team_id = t.team_id THEN 1 ELSE 0 END) AS matches_won
	FROM ipl.Teams t
	JOIN ipl.Matches m ON t.team_id = m.team1_id OR t.team_id = m.team2_id
	GROUP BY t.team_name;

SELECT * FROM ipl.TeamPerformance;
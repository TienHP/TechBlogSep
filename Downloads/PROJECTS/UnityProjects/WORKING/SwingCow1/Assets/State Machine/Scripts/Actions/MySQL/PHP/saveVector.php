<?php
	require_once("config.php");

	$auth_host = $GLOBALS['auth_host'];
	$auth_user = $GLOBALS['auth_user'];
	$auth_pass = $GLOBALS['auth_pass'];
	$auth_dbase = $GLOBALS['auth_dbase'];

	$db = mysql_connect($auth_host, $auth_user, $auth_pass) or die (mysql_error());
	mysql_select_db($auth_dbase,$db);
	
	$key = mysql_real_escape_string($_POST['key']);
	$x = mysql_real_escape_string($_POST['x']);
	$y = mysql_real_escape_string($_POST['y']);
	$z = mysql_real_escape_string($_POST['z']);

	mysql_query("DELETE FROM vectors WHERE name = '$key'");
	mysql_query("INSERT INTO vectors(name,x,y,z) VALUES ('$key','$x','$y','$z')");
	mysql_close($db);
?> 
 
DELIMITER $$
DROP PROCEDURE IF EXISTS `updateuserid`$$
CREATE  PROCEDURE `updateuserid`()
BEGIN
    #Routine body goes here...
    DECLARE c INT;
    DECLARE i INT;
    DECLARE maxid2 INT;
    DECLARE fav_id2 INT;
    DECLARE user_id2 VARCHAR(40);
    DROP TABLE IF EXISTS  tmp_table;
    CREATE TEMPORARY TABLE tmp_table (  
id INT NOT NULL AUTO_INCREMENT,  
fav_id INT   ,
user_id VARCHAR(40),
PRIMARY KEY(id)
) ; 
    INSERT INTO tmp_table(fav_id,user_id) SELECT fav_id,user_id FROM tbl_my_favorites WHERE user_id!="" AND user_id IS NOT NULL AND user_id!='undefined';
    SELECT MAX(id) INTO @maxid2 FROM tmp_table;
    SET i=1;
    WHILE i <= @maxid2 DO

    SELECT fav_id,user_id INTO @fav_id2,@user_id2 FROM tmp_table WHERE  id=i ;
          IF @user_id2 != "" AND @user_id2 IS NOT NULL THEN
             UPDATE tbl_my_favorites SET user_id=MD5(CONVERT("drc"+@user_id2+"bank" USING utf8)) WHERE fav_id=@fav_id2;
          END IF;
    SET i = i + 1;
    END WHILE ;
    DROP TABLE IF EXISTS  tmp_table;
END$$

DELIMITER ;

CALL updateuserid();
DROP PROCEDURE IF EXISTS updateuserid; 
-- Adminer 4.8.1 MySQL 10.3.39-MariaDB-0ubuntu0.20.04.2 dump

SET NAMES utf8;
SET time_zone = '+00:00';
SET foreign_key_checks = 0;
SET sql_mode = 'NO_AUTO_VALUE_ON_ZERO';

SET NAMES utf8mb4;

DROP DATABASE IF EXISTS `soft_database`;
CREATE DATABASE `soft_database` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci */;
USE `soft_database`;

DELIMITER ;;

CREATE FUNCTION `translation_get`(`p_id` smallint unsigned, `p_language_id` smallint unsigned) RETURNS varchar(200) CHARSET utf8mb4 COLLATE utf8mb4_unicode_ci
BEGIN 
DECLARE language_id varchar(200);
select common_translationId  into language_id from common_language cl 
where cl.common_translationId = p_language_id;
select 
CASE 
	when language_id = '1' then ct.arabic  
	when language_id= '7' then ct.english  
	/*
	when language_name = 'bulgarian' then bulgarian
	when language_name = 'croatian' then croatian
	when language_name = 'czech' then czech
	when language_name = 'danish' then danish
	when language_name = 'dutch' then dutch
	
	when language_name = 'finnish' then finnish
	when language_name = 'french' then french
	when language_name = 'german' then german
	when language_name = 'greek' then greek
	when language_name = 'hebrew' then hebrew
	when language_name = 'hungarian' then hungarian
	when language_name = 'indonesian' then indonesian
	when language_name = 'italian' then italian
	when language_name = 'japanese' then japanese
	when language_name = 'korean' then korean
	when language_name = 'latvian' then latvian
	when language_name = 'lithuanian' then lithuanian
	when language_name = 'mandarin' then mandarin
	when language_name = 'norwegian' then norwegian
	when language_name = 'polish' then polish
	when language_name = 'portuguese' then portuguese
	when language_name = 'romanian' then romanian
	when language_name = 'russian' then russian
	when language_name = 'slovak' then slovak
	when language_name = 'slovenian' then slovenian
	when language_name = 'spanish' then spanish
	when language_name = 'swedish' then swedish
	when language_name = 'thai' then thai
	when language_name = 'turkish' then turkish
	when language_name = 'vietnamese' then vietnamese
	*/
	else ''
end translated_name into @outvar
from common_translation ct
where ct.id = p_id;
return @outvar;
end;;

CREATE PROCEDURE `center_user_list`()
BEGIN

 	SELECT scu.*, sr.role_name, ct.english AS language_name FROM 
		sales_center_user AS scu
		LEFT JOIN sales_role AS sr ON sr.id = scu.sales_roleId
		LEFT JOIN common_language AS cl ON cl.common_translationId = scu.common_languageId
		LEFT JOIN common_translation AS ct ON ct.id = cl.common_translationId;

END;;

CREATE PROCEDURE `common_country_list`()
BEGIN

	SELECT cc.*, ct.english, ct.arabic FROM common_country AS cc
	LEFT JOIN common_translation AS ct ON ct.id = cc.common_translationId;

END;;

CREATE PROCEDURE `common_location_by_countryId`(
	IN `countryId` INT
)
BEGIN

	SELECT * FROM common_location WHERE common_countryId = countryId;

END;;

CREATE PROCEDURE `customerGetByUserID`(IN `UserID` int)
BEGIN
SELECT *
FROM marketing_user_customer
WHERE
	sales_userId = UserID;
END;;

CREATE PROCEDURE `customerInsert`(IN `sales_userId` int(11), IN `common_countryId` int(11), IN `phone` varchar(15), IN `name` varchar(20), IN `phone_alternate` varchar(15), IN `email` varchar(45), IN `last_visit` varchar(10), IN `added` datetime, IN `birthday` varchar(10), IN `age` int(11), IN `address` varchar(100), IN `details` varchar(100), IN `common_app_messagingId` int(11), IN `common_app_socialId` int(11), IN `send_events_msg` bit(1), IN `send_feedback_msg` bit(1), IN `send_promotion_msg` bit(1), IN `send_reminder_msg` bit(1), IN `reminder_duration` varchar(20), IN `reminder_count` int(11), IN `last_reminder` datetime)
BEGIN

insert into marketing_user_customer (
sales_userId,
common_countryId,
phone,
name,
phone_alternate,
email,
last_visit,
added,
birthday,
age,
address,
details,
common_app_messagingId,
common_app_socialId,
send_events_msg,
send_feedback_msg,
send_promotion_msg,
send_reminder_msg,
reminder_duration,
reminder_count,
last_reminder)
values (sales_userId,
 common_countryId,
 phone,
 name,
 phone_alternate,
 email,
 last_visit,
 added,
 birthday,
 age,
 address,
 details,
 common_app_messagingId,
 common_app_socialId,
 send_events_msg,
 send_feedback_msg,
 send_promotion_msg,
 send_reminder_msg,
 reminder_duration,
 reminder_count,
 last_reminder);

SET @id = @@IDENTITY;

SELECT @@IDENTITY;        


END;;

CREATE PROCEDURE `customerUpdate`(IN `ID_VAL` int(11), IN `SALES_USER_ID_VAL` int(11), IN `COMMON_COUNTRY_ID` int(11), IN `PHONE` varchar(15), IN `NAME` varchar(20), IN `PHONE_ALTERNATE` varchar(15), IN `EMAIL` varchar(45), IN `LAST_VISIT` varchar(10), IN `ADDED` datetime, IN `AGE_BIRTHDAY` varchar(10), IN `ADDRESS` varchar(100), IN `DETAILS` varchar(100), IN `COMMON_APP_MESSAGING_ID` int(11), IN `COMMON_APP_SOCIAL_ID` int(11), IN `EVENTS` varchar(20), IN `FEEDBACK` varchar(20), IN `PROMOTIONS` varchar(20), IN `REMINDER_DURATION` varchar(20), IN `REMINDER_TIMES` varchar(20))
BEGIN
UPDATE marketing_user_customer SET
    common_countryId = COMMON_COUNTRY_ID,
    phone = PHONE,
    name = NAME,
    phone_alternate = PHONE_ALTERNATE,
    email = EMAIL,
    last_visit = LAST_VISIT,
    added = ADDED,
    age_birthday = AGE_BIRTHDAY,
    address = ADDRESS,
    details = DETAILS,
    common_app_messagingId =COMMON_APP_MESSAGING_ID,
    common_app_socialId = COMMON_APP_SOCIAL_ID,
    events = EVENTS,
    feedback = FEEDBACK,
    promotions = PROMOTIONS,
    reminder_duration = REMINDER_DURATION,
    reminder_times = REMINDER_TIMES
WHERE
	id= ID_VAL and sales_userId = SALES_USER_ID_VAL;
END;;

CREATE PROCEDURE `eventUpdate`(eventid int, 
	eventname varchar(45), 
	eventdetail varchar(500) )
BEGIN
UPDATE event SET
	eventname = eventname,
	eventdetail = eventdetail
	
WHERE
	eventid = eventid;
END;;

CREATE PROCEDURE `GetAllTemplates2`(IN `languageId` int(11), IN `userId` int(11))
BEGIN

SELECT em.id as event_message_id, em.message_heading as default_heading, em.message as default_message,
cem.message_heading as custom_heading, cem.message as custom_message
FROM eventMessage as em
LEFT JOIN customEventMessage as cem
ON (em.id = cem.eventMessageId and cem.sales_userId = userId) 
 where em.languageId = languageId or (cem.eventDateId = 6);
END;;

CREATE PROCEDURE `GetCountriesByLaunched`()
BEGIN

SELECT DISTINCT common_country.common_translationId, common_translation.english
FROM common_country
INNER JOIN sales_center_detail ON common_country.common_translationId = sales_center_detail.common_countryId
INNER JOIN sales_plan_country ON common_country.common_translationId = sales_plan_country.common_countryId
INNER JOIN common_translation ON common_country.common_translationId = common_translation.id;

END;;

CREATE PROCEDURE `getGlobalCustomAndDefoultMessage`(IN `userId` int(11), IN `templateNameId` int(11))
BEGIN
SELECT custom_template as CustomMessage from marketing_user_template 
where sales_userId = userId and marketing_global_templateId = 
(select id from marketing_global_template 
where marketing_template_nameId = templateNameId
and common_languageId = (select value from marketing_user_setting where sales_userId = userId
and marketing_setting_nameId = 6)
);
select template as DefaultMessage from marketing_global_template 
where marketing_template_nameId = templateNameId
and common_languageId = (select value from marketing_user_setting where sales_userId = userId
and marketing_setting_nameId = 6);
END;;

CREATE PROCEDURE `getRelatedMessages`(IN `countryId` int(11), IN `eventMessId` int(11), IN `userId` int(11), IN `languageId` int(11))
BEGIN

select eventMessageId, eventDateId, message_heading as heading,message
from customEventMessage where (sales_userId = userId) or (sales_userId is null and country_id = sales_userId );


select eventMessageId, eventDateId, message_heading as heading,message
from customEventMessage where country_id = countryId and eventMessageId = eventMessId and sales_userId is null;


end;;

CREATE PROCEDURE `get_available_sales_center`(
	IN `countryval` int(11)
)
BEGIN
	
select id,center_name  as availablecenter from sales_center 
where common_countryId=countryval and active=1
and id not in 
	(select * from
		(
			select id as assignedcenter from sales_center sc where 
			FIND_IN_SET(id,(select GROUP_CONCAT(primary_sales_centerId)  from sales_center_user scu ))
			and  common_countryId =countryval
		UNION 
			select id as assignedcenter from sales_center sc where 
			FIND_IN_SET(id,(select GROUP_CONCAT(secondary_sales_centerId)  from sales_center_user scu )) 
			and  common_countryId =countryval
		) getcenters
	where assignedcenter is not NULL);


END;;

CREATE PROCEDURE `get_center_manager_list`(
	IN `centers` VARCHAR(50)
)
BEGIN

SELECT scu.*, sc.center_name FROM sales_center_user scu
	LEFT JOIN sales_center sc ON sc.id = scu.primary_sales_centerId
		WHERE scu.primary_sales_centerId IN (centers)
		AND scu.sales_user_typeId = 1;

END;;

CREATE PROCEDURE `get_country_from_sales_center`(in salescenter int(11))
BEGIN
select common_countryId  from sales_center sc 
where id=salescenter and sc.active =1 ;
END;;

CREATE PROCEDURE `get_hiring_post_detail_by_id`(
	IN `hiring_post_id` INT
)
BEGIN

SELECT shp.*, shwn.name AS website_name, sc.center_name 
	FROM sales_hiring_post AS shp
		LEFT JOIN sales_hiring_website_country AS shwc ON shwc.id = shp.website_countryId
		LEFT JOIN sales_hiring_website_name AS shwn ON shwn.id = shwc.website_nameId
		LEFT JOIN sales_center AS sc ON sc.id = shp.sales_centerId
	WHERE shp.id = hiring_post_id;

END;;

CREATE PROCEDURE `get_hiring_post_list_by_user_center`(
	IN `centers` INT
)
BEGIN
	
SELECT shp.*, ct.english AS country_name, sc.center_name  FROM sales_hiring_post AS shp
	LEFT JOIN sales_hiring_website_country as hwc ON hwc.id = shp.website_countryId
	LEFT JOIN common_country AS cc ON cc.common_translationId = hwc.common_countryId
	LEFT JOIN common_translation AS ct ON ct.id = cc.common_translationId
	LEFT JOIN sales_center AS sc ON sc.id = shp.sales_centerId
WHERE sales_centerId IN(centers);

END;;

CREATE PROCEDURE `get_sales_country_website`(in commoncountryid int(11))
BEGIN
	
select shwn.name,shwc.id as websitecountryid from sales_hiring_website_country shwc 
left join sales_hiring_website_name shwn on shwn.id =shwc.website_nameId 
where common_countryId=commoncountryid
and shwn.status =1 and shwc.status =1;

END;;

CREATE PROCEDURE `get_sch_msg_by_date`(IN `userid` int(11))
BEGIN

	
select mum.id , mut.id as usertemplate_id,mum.sales_userId,
ifnull(mut.NAME,mtd.name) as name,
ifnull(ifnull(mut.template,mtc.template),mtd.template)  as template,
mut.add_customer,
mut.add_owner,
mut.add_business,
mut.send_text_with_image,
mtt.`type` templatetype,
mtt.id as templatetypeid,
mum.marketing_user_customerId ,
mum.sent,
mum.scheduled_time,
muc.phone_countryId as customer_countryid,
muc.name customer_name,
ct.english as country_name,
muc.phone,
muc.phone_alternate 
from 
marketing_user_message mum left join 
marketing_user_template mut on mum.marketing_user_templateId = mut.id 
left join marketing_template_country mtc on mtc.id =mut.marketing_template_countryId 
left JOIN marketing_template_detail mtd ON mtd.id =ifnull(mtc.marketing_template_detailId,mut.marketing_template_detailId)
left join marketing_template_type mtt on mtt.id =mtd.marketing_template_typeId
left join marketing_user_customer muc on muc.id =mum.marketing_user_customerId 
left join common_translation ct on ct.id= muc.phone_countryId
where mum.sales_userId =userid and DATE(mum.scheduled_time) = Date(now()) order by mum.id desc;

END;;

CREATE PROCEDURE `get_user_center_list`(
	IN `centers` INT
)
BEGIN

	SELECT * FROM sales_center WHERE id IN(centers);

END;;

CREATE PROCEDURE `get_user_settings`(IN `userId` int(11))
BEGIN 
SELECT
su.send_feedback,
su.send_birthday,
su.send_events,
su.send_advertisement,
su.send_reminders,
delete_customer ,
reminder_duration ,
reminder_times 
from sales_user as su where su.id = userId ;
end;;

CREATE PROCEDURE `listing_category_child`(pr_category_detail_id smallint(5))
BEGIN
select lcd.id, lcd.name
,COALESCE ((select 1 from dual 
	WHERE exists 
		(select 1 from listing_category_relation lcrs 
		where lcrs.parent_listing_category_detailId = lcd.id)),0) has_child
from listing_category_detail lcd
join listing_category_relation lcr on lcd.id=lcr.listing_category_detailId 
where lcr.parent_listing_category_detailId  = pr_category_detail_id;
END;;

CREATE PROCEDURE `listing_category_main`(IN `category_type` bit)
BEGIN

SELECT listing_category_detail.id, listing_category_detail.name
FROM listing_category_detail
INNER JOIN listing_category_main ON listing_category_detail.id = listing_category_main.listing_category_detailId
WHERE listing_category_main.listing_category_typeId = category_type;

END;;

CREATE PROCEDURE `listing_category_parent`(IN pr_category_type  bit(1))
BEGIN
select lcm.id, lcd.name, lbt.`type`  from listing_category_main lcm 
join listing_category_detail lcd on lcm.id=lcd.id
join listing_business_type lbt on lcm.listing_category_typeId = lbt.id 
where lbt.id = pr_category_type;
END;;

CREATE PROCEDURE `listing_user_category_delete`(IN `p_sales_userId` int(11), IN `p_listing_category_detailId` int(11))
BEGIN
	delete from listing_user_category  where sales_userId = p_sales_userId and 
		listing_category_detailId = p_listing_category_detailId;
	delete mut from marketing_user_template mut where mut.sales_userId = p_sales_userId
		and EXISTS (
			select 1 from marketing_template_detail mtd 
			where mtd.listing_category_detailId = p_listing_category_detailId and mtd.id = mut.marketing_template_detailId 
		);
	SELECT concat('success')  as result ;
	
END;;

CREATE PROCEDURE `listing_user_category_getall`(IN `sales_userId` int unsigned)
BEGIN

select listing_category_detailId,name,concat('[', group_concat(json_object("id",marketing_template_detail_id,"assigned_to_user",assigned_to_user,"template",template,"image",image)) ,']') templates
from
(SELECT luc.listing_category_detailId, lcd.name, mtd.id marketing_template_detail_id, mut.id marketing_user_template_id, COALESCE (mut.template,mtd.template ) template
,case when mut.id is null then 0 else 1 end assigned_to_user
, case when mut.id is null then 
	case when mtd.id is null then '' else concat('/home/business1.app/data.business1.app/template/detail/',mtd.id,'.png')  end
	else concat('/home/business1.app/data.business1.app/template/user/',mut.id,'.png') end image
FROM listing_user_category luc
INNER JOIN listing_category_detail lcd ON luc.listing_category_detailId = lcd.id
LEFT join marketing_template_detail mtd on luc.listing_category_detailId = mtd.listing_category_detailId
LEFT join marketing_user_template mut on mut.marketing_template_detailId =mtd.id and mut.sales_userId = luc.sales_userId 
WHERE luc.sales_userId = sales_userId
) a group by listing_category_detailId,name;

END;;

CREATE PROCEDURE `listing_user_category_insert`(IN `p_sales_userId` int(11), IN `p_listing_category_detailId` int(11))
proc_label:BEGIN
	DECLARE checkcount int;
	DECLARE isparent int;
	
	select count(listing_category_detailId) into checkcount FROM listing_user_category luc WHERE luc.sales_userId = p_sales_userId;
	if checkcount >= 4 then
		select 'error_maximum_category_reached|p_sales_userId' as result;
		leave proc_label;
	end if;
	

	select count(*) into isparent from dual
	where EXISTS (
	select lcr.listing_category_detailId 
	from listing_category_relation lcr where lcr.parent_listing_category_detailId = p_listing_category_detailId);
	
	if isparent = 1 then
		select 'error_categories_must_be_last_categories|p_listing_category_detailId' as result;
		leave proc_label;
	end if;
	insert into listing_user_category(sales_userId,listing_category_detailId) values (p_sales_userId,p_listing_category_detailId);
      
	SELECT 'success' as result ;
END;;

CREATE PROCEDURE `listing_user_category_template_getall`(IN `p_userid` int(11))
BEGIN

select listing_category_detailId, TO_BASE64(image) image,assigned_to_user,marketing_user_template_id,marketing_template_detailId, template
from
(SELECT  mut.id marketing_user_template_id,mtd.id marketing_template_detailId,luc.listing_category_detailId, COALESCE (mut.template,mtd.template ) template
,case when mut.id is null then 0 else 1 end assigned_to_user
, COALESCE(LOAD_FILE(concat('/home/business1.app/data.business1.app/template/user/',mut.id,'.png')),LOAD_FILE(concat('/home/business1.app/data.business1.app/template/detail/',mtd.id,'.png'))) image
FROM listing_user_category luc
INNER JOIN listing_category_detail lcd ON luc.listing_category_detailId = lcd.id
LEFT join marketing_template_detail mtd on luc.listing_category_detailId = mtd.listing_category_detailId
LEFT join marketing_user_template mut on mut.marketing_template_detailId =mtd.id and mut.sales_userId = luc.sales_userId 
WHERE luc.sales_userId = p_userid
) a ;

END;;

CREATE PROCEDURE `marketing_category_insert`(IN par_category_level int(1), IN par_category_name varchar(200), In par_parent_id mediumint(9))
BEGIN
	DECLARE checkcategory_id int;
	set checkcategory_id=-1;
	select mc.id into checkcategory_id from marketing_category mc  where mc.category_name = par_category_name;

	if (checkcategory_id = -1) then 
		select 'existing_category|pr_category_name' as result;
	end if;
	
	if par_category_level != 1 then
		select mc.id into checkcategory_id from marketing_category mc  where mc.id = par_parent_id;
		if (checkcategory_id = -1) then 
			select 'not_existing_parent_category|par_parent_id' as result;
		end if;
	end if;
-- 	SELECT LAST_INSERT_ID() as result; 
	
END;;

CREATE PROCEDURE `marketing_template_date_getall`(IN `userid` int(11))
BEGIN
select mtd.id,mtd.date
,mut.id marketing_user_template_id
from marketing_template_date mtd 
join marketing_user_template mut on mut.marketing_template_countryId = mtd.marketing_template_countryId 
where 
mut.sales_userId=userid;
END;;

CREATE PROCEDURE `marketing_template_detail_by_id`(
	IN `marketingtemplateid` INT
)
BEGIN

	SELECT mtd.*, mtt.type AS marketing_template_type_name, cl.name AS com_language, lcd.name AS category_name
	FROM marketing_template_detail AS mtd
		LEFT JOIN marketing_template_type AS mtt ON mtt.id = mtd.marketing_template_typeId
		LEFT JOIN common_language AS cl ON cl.id = mtd.common_languageId
		LEFT JOIN listing_category_detail AS lcd ON lcd.id = mtd.listing_category_detailId
		WHERE mtd.id = marketingtemplateid;

END;;

CREATE PROCEDURE `marketing_template_detail_list`()
BEGIN

	SELECT mtd.*, mtt.type AS marketing_template_type_name, cl.name AS com_language, lcd.name AS category_name
	FROM marketing_template_detail AS mtd
		LEFT JOIN marketing_template_type AS mtt ON mtt.id = mtd.marketing_template_typeId
		LEFT JOIN common_language AS cl ON cl.id = mtd.common_languageId
		LEFT JOIN listing_category_detail AS lcd ON lcd.id = mtd.listing_category_detailId;

END;;

CREATE PROCEDURE `marketing_user_advertise_template_insert`(IN `p_sales_userId` int(11), IN `p_marketing_template_detailId` int(11))
proc_label:BEGIN
	
	DECLARE next_marketing_template_typeId int;

	select mad.order into next_marketing_template_typeId from marketing_advertisement_detail mad where not exists 
	(select marketing_advertisement_detailId FROM marketing_user_template mut WHERE mut.sales_userId = p_sales_userId
	and mut.marketing_advertisement_detailId = mad.order) order by mad.order LIMIT 1;

	if next_marketing_template_typeId is NULL then
		select 'error_maximum_advertisement_reached|p_sales_userId' as result;
		leave proc_label;
	end if;
	
	insert into marketing_user_template(sales_userId,marketing_template_detailId,marketing_advertisement_detailId,add_customer,add_owner,add_business,send_text_with_image) values (p_sales_userId,p_marketing_template_detailId,next_marketing_template_typeId,0,0,0,0);
SET @id = @@IDENTITY;

SELECT @@IDENTITY;   

END;;

CREATE PROCEDURE `marketing_user_customer_delete_reminder`()
BEGIN
		 
	select muc.id as customerid,muc.phone,muc.name,muc.sales_userId 
	from marketing_user_customer muc 
	 left join marketing_user_setting mus on mus.sales_userId =muc.sales_userId 
	 where 
	 muc.send_reminder_msg ='Yes'
	 and datediff(now(),greatest(last_reminder,last_visit))>=(ifnull(muc.reminder_duration,mus.reminder_duration) *30)
	 and muc.reminder_count = mus.reminder_times 
	 and mus.send_reminders = 'Yes'
	 and mus.delete_customer='Yes';
END;;

CREATE PROCEDURE `marketing_user_customer_send_reminder`()
BEGIN
	
	select muc.id as customerid,muc.phone,muc.name,muc.sales_userId,
	temp.id as user_templateid
	from marketing_user_customer muc 
	left join marketing_user_setting mus on mus.sales_userId =muc.sales_userId 
left join (	select mut2.* from marketing_user_template mut2 
			left join marketing_template_detail mtd2 on mut2.marketing_template_detailId = mtd2.id 
			where marketing_template_typeId=9
			)temp on temp.sales_userId=muc.sales_userId 
	 where 
	 muc.send_reminder_msg ='Yes'
	 and datediff(now(),greatest(last_reminder,last_visit))>=(ifnull(muc.reminder_duration,mus.reminder_duration) *30)
	 and muc.reminder_count < mus.reminder_times 
	 and mus.send_reminders = 'Yes';
	
END;;

CREATE PROCEDURE `marketing_user_customer_update_reminder`(in customerid int,in salesuserid int)
BEGIN
	
	declare getcount int;
	
	select COUNT(reminder_count) into getcount from  
	marketing_user_customer muc where id=customerid and sales_userId =salesuserid;
	

	update marketing_user_customer set reminder_count=getcount + 1, 
	last_reminder = DATE_FORMAT(now(), "%Y-%m-%d")
	where id=customerid and sales_userId=salesuserid;
END;;

CREATE PROCEDURE `marketing_user_message_delete`(in usermessage_id int,in salesuserid int)
BEGIN
	
	delete from marketing_user_message where id=usermessage_id and sales_userId = salesuserid ;
	
END;;

CREATE PROCEDURE `marketing_user_message_insert`(IN `salesuserid` int, IN `customerid` int, IN `marketingusertemplateid` int, IN `scheduledtime` datetime)
BEGIN
	
	DECLARE checktemplateid varchar(10);
	set checktemplateid='';

	select id into checktemplateid from marketing_user_template where id=marketingusertemplateid;
	
		if(checktemplateid='') then 
		select 'template_not_exist|id' as result;
		else
		insert into marketing_user_message  (sales_userid,marketing_user_customerId,marketing_user_templateId,scheduled_time) 
		values (salesuserid,customerid,marketingusertemplateid,scheduledtime);

SET @id = @@IDENTITY;

SELECT @@IDENTITY as result; 
		end if;
	
END;;

CREATE PROCEDURE `marketing_user_message_scheduled_getall`(IN `userid` int(11))
BEGIN
	
select mum.id , mut.id as usertemplate_id,mum.sales_userId,
ifnull(mut.NAME,mtd.name) as name,
ifnull(ifnull(mut.template,mtc.template),mtd.template)  as template,
mtt.`type` templatetype,
mtt.id as templatetypeid,
mum.marketing_user_customerId ,
mum.sent,
mum.scheduled_time,
muc.phone_countryId as customer_countryid,
muc.name customer_name,
ct.english as country_name,
muc.phone,
muc.phone_alternate 
from 
marketing_user_message mum left join 
marketing_user_template mut on mum.marketing_user_templateId = mut.id 
left join marketing_template_country mtc on mtc.id =mut.marketing_template_countryId 
left JOIN marketing_template_detail mtd ON mtd.id =ifnull(mtc.marketing_template_detailId,mut.marketing_template_detailId)
left join marketing_template_type mtt on mtt.id =mtd.marketing_template_typeId
left join marketing_user_customer muc on muc.id =mum.marketing_user_customerId 
left join common_translation ct on ct.id= muc.phone_countryId
where mum.sales_userId =userid order by mum.id desc;

END;;

CREATE PROCEDURE `marketing_user_message_scheduled_getall.old`(IN `userid` int(11))
BEGIN
	
select mum.id , mut.id as usertemplate_id,mum.sales_userId,
ifnull(mut.NAME,mtd.name) as name,
ifnull(ifnull(mut.template,mtc.template),mtd.template)  as template,
mtt.`type` templatetype,
mtt.id as templatetypeid,
mum.marketing_user_customerId ,
mum.sent,
mum.scheduled_time,
muc.phone_countryId as customer_countryid,
cc.name as customer_country_name,
muc.name customer_name,
cc.name as country_name,
muc.phone,
muc.phone_alternate 
from 
marketing_user_message mum left join 
marketing_user_template mut on mum.marketing_user_templateId = mut.id 
left join marketing_template_country mtc on mtc.id =mut.marketing_template_countryId 
left JOIN marketing_template_detail mtd ON mtd.id =ifnull(mtc.marketing_template_detailId,mut.marketing_template_detailId)
left join marketing_template_type mtt on mtt.id =mtd.marketing_template_typeId
left join marketing_user_customer muc on muc.id =mum.marketing_user_customerId 
left join common_country cc on cc.id=muc.phone_countryId
where mum.sales_userId =userid order by mum.id desc;

END;;

CREATE PROCEDURE `marketing_user_message_scheduled_preview`(in user_message_id int(11), in userid int(11))
BEGIN
	
select mum.id , mut.id as usertemplate_id,mum.sales_userId,
ifnull(mut.NAME,mtd.name) as name,
ifnull(ifnull(mut.template,mtc.template),mtd.template)  as template,
mtt.`type` templatetype,
mtt.id as templatetypeid
from 
marketing_user_message mum left join 
marketing_user_template mut on mum.marketing_user_templateId = mut.id 
inner join marketing_template_country mtc on mtc.id =mut.marketing_template_countryId 
left JOIN marketing_template_detail mtd ON mtd.id =mtc.marketing_template_detailId 
left join marketing_template_type mtt on mtt.id =mtd.marketing_template_typeId
where mum.sales_userId =userid and mum.id=user_message_id
and mum.sent is NULL ;
	
END;;

CREATE PROCEDURE `marketing_user_message_update`(IN `usermessage_id` int, IN `salesuserid` varchar(10), IN `customerid` varchar(10), IN `marketingusertemplateid` varchar(10), IN `scheduledtime` datetime)
BEGIN
	
	DECLARE usermessagecheck varchar(10);
	DECLARE salesusercheck varchar(10);
	set usermessagecheck='';

	
	set salesusercheck='';

	if salesuserid='' then
	set salesuserid=NULL ;
	end if;

	if customerid='' then
	set customerid=NULL ;
	end if;

	if marketingusertemplateid='' then
	set marketingusertemplateid=NULL ;
	end if;

	if scheduledtime='' then
	set scheduledtime=NULL ;
	end if;

			-- condition to check whether id belongs to the intended sales user 
		select sales_userId into salesusercheck from marketing_user_message where id=usermessage_id;	
	
		if (salesusercheck != salesuserid)	then
			select 'sales user does not match' as result;
		
		else


				select sent into usermessagecheck from marketing_user_message where id=usermessage_id;
	
			-- check if message is already sent cannot update message
				if(usermessagecheck='Yes') then 
					select 'Message already sent' as result;
	
				else
	
					update marketing_user_message set marketing_user_customerId =ifnull(customerid,marketing_user_customerId),
					marketing_user_templateId =ifnull(marketingusertemplateid,marketing_user_templateId),
					scheduled_time = ifnull(scheduledtime,scheduled_time) 
					where id=usermessage_id;
		
				end if;
		end if;
END;;

CREATE PROCEDURE `marketing_user_template_country_getall`(IN `salesuserid` tinyint, IN `commoncountryid` tinyint)
BEGIN
	
select mtc.id,common_countryId,nvl(nvl(mut.template,mtc.template),mtd.template) as template,
case when mut.sales_userId is not null then true else false end as assigned_to_user,
case when mut.sales_userId is not null then mut.id else 0 end as user_template_id,
mtd.name 
from marketing_template_country mtc 
left join marketing_user_template mut on mtc.id=mut.marketing_template_countryId and sales_userid=salesuserid
left join marketing_template_detail mtd on  mtd.id = mtc.marketing_template_detailId 
where mtc.common_countryId=commoncountryid ;

END;;

CREATE PROCEDURE `marketing_user_template_country_insert`(in countrytemplateid varchar(500),in salesuserid int)
BEGIN
	
declare getcountrytemplate varchar(50);

WHILE LENGTH(countrytemplateid) > 0 DO



        SET getcountrytemplate = LEFT(countrytemplateid, LOCATE (',', countrytemplateid+',')-1);
       

       -- insert into marketing_user_template (sales_userid,marketing_template_countryId)
       -- values (salesuserid,getcountrytemplate);
       select LENGTH(countrytemplateid);
       
       SET countrytemplateid = INSERT(countrytemplateid, 1, LOCATE (',', countrytemplateid+','), '');
      
      END WHILE;
END;;

CREATE PROCEDURE `marketing_user_template_delete`(in usertemplate_id int,in salesuserid int)
BEGIN
	delete from marketing_user_template where id=usertemplate_id and sales_userId=salesuserid ;
END;;

CREATE PROCEDURE `marketing_user_template_getall`(IN `userid` int(11))
BEGIN
select * FROM 
(
select mut.id as usertemplate_id,mut.sales_userId,
COALESCE (mut.NAME,mtd.name,translation_get(mad.common_translationId,mtd.common_languageId)) as name,
COALESCE(mut.template,mtd.template) as template,
mtt.`type` templatetype,
mtt.id templatetypeid,
mut.last_update_image,
mut.sending_date,
mut.sending_year
from marketing_user_template mut 
inner JOIN marketing_template_detail mtd ON mtd.id =mut.marketing_template_detailId 
left join marketing_template_type mtt on mtt.id =mtd.marketing_template_typeId
left join marketing_advertisement_detail mad on mad.marketing_template_typeId = mtt.id and mad.`order` =mut.marketing_advertisement_detailId 
where sales_userId =userid
union
select mut.id as usertemplate_id,sales_userId,
COALESCE(mut.NAME,mtd.name,translation_get(mad.common_translationId,mtd.common_languageId)) as name,
COALESCE(mut.template,mtc.template,mtd.template)  as template,
mtt.`type` templatetype,
mtt.id as templatetypeid,
mut.last_update_image,
ifnull(ifnull(mtc.date,mut.sending_date),mtd.date) sending_date,
case when marketing_template_countryId is not null then '' 
else mut.sending_year end sending_year
from marketing_user_template mut 
inner join marketing_template_country mtc on mtc.id =mut.marketing_template_countryId 
left JOIN marketing_template_detail mtd ON mtd.id =ifnull(mtc.marketing_template_detailId,mut.marketing_template_detailId)
left join marketing_template_type mtt on mtt.id =mtd.marketing_template_typeId
left join marketing_advertisement_detail mad on mad.marketing_template_typeId = mtt.id and mad.`order` =mut.marketing_advertisement_detailId 
where sales_userId =userid
UNION 
select mut.id as usertemplate_id,
sales_userId,
mut.name as name,
mut.template as template,
'custom'  as templatetype,
'0' as templatetypeid
,mut.last_update_image,
mut.sending_date,
mut.sending_year
from marketing_user_template mut 
where sales_userId =userid
and mut.marketing_template_countryId is null and 
mut.marketing_template_detailId is null
)A
order by A.templatetypeid, A.usertemplate_id;
END;;

CREATE PROCEDURE `marketing_user_template_getall_images`(IN `userid` int)
BEGIN
select * FROM 
(
select mut.id as usertemplate_id,mut.sales_userId, TO_BASE64(COALESCE(load_file(CONCAT('/home/business1.app/data.business1.app/template/user/',mut.id,'.png')),
load_file(CONCAT('/home/business1.app/data.business1.app/template/detail/',mtd.id,'.png'))))
as image,mut.last_update_image, mut.add_customer, mut.add_owner, mut.add_business, mut.send_text_with_image,
COALESCE (mut.NAME,mtd.name,translation_get(mad.common_translationId,mtd.common_languageId)) as name,
COALESCE(mut.template,mtd.template) as template,
mtt.`type` templatetype,
mtt.id templatetypeid,
mut.sending_date,
mut.sending_year
from marketing_user_template mut 
inner JOIN marketing_template_detail mtd ON mtd.id =mut.marketing_template_detailId 
left join marketing_template_type mtt on mtt.id =mtd.marketing_template_typeId
left join marketing_advertisement_detail mad on mad.marketing_template_typeId = mtt.id and mad.`order` =mut.marketing_advertisement_detailId 
where sales_userId =userid
union
select mut.id as usertemplate_id,sales_userId, TO_BASE64(COALESCE(load_file(CONCAT('/home/business1.app/data.business1.app/template/user/',mut.id,'.png')),
load_file(CONCAT('/home/business1.app/data.business1.app/template/country/',mtc.id,'.png')),
load_file(CONCAT('/home/business1.app/data.business1.app/template/detail/',mtd.id,'.png'))))
as image,mut.last_update_image, mut.add_customer, mut.add_owner, mut.add_business, mut.send_text_with_image,
COALESCE(mut.NAME,mtd.name,translation_get(mad.common_translationId,mtd.common_languageId)) as name,
COALESCE(mut.template,mtc.template,mtd.template)  as template,
mtt.`type` templatetype,
mtt.id as templatetypeid,
ifnull(ifnull(mtc.date,mut.sending_date),mtd.date) sending_date,
case when marketing_template_countryId is not null then '' 
else mut.sending_year end sending_year
from marketing_user_template mut 
inner join marketing_template_country mtc on mtc.id =mut.marketing_template_countryId 
left JOIN marketing_template_detail mtd ON mtd.id =ifnull(mtc.marketing_template_detailId,mut.marketing_template_detailId)
left join marketing_template_type mtt on mtt.id =mtd.marketing_template_typeId
left join marketing_advertisement_detail mad on mad.marketing_template_typeId = mtt.id and mad.`order` =mut.marketing_advertisement_detailId 
where sales_userId =userid
UNION 
select mut.id as usertemplate_id,
sales_userId, TO_BASE64(COALESCE(load_file(CONCAT('/home/business1.app/data.business1.app/template/user/',mut.id,'.png'))
))
as image,mut.last_update_image, mut.add_customer, mut.add_owner, mut.add_business, mut.send_text_with_image,
mut.name as name,
mut.template as template,
'custom'  as templatetype,
'0' as templatetypeid,
mut.sending_date,
mut.sending_year
from marketing_user_template mut 
where sales_userId =userid
and mut.marketing_template_countryId is null and 
mut.marketing_template_detailId is null
)A
order by A.templatetypeid, A.usertemplate_id;
END;;

CREATE PROCEDURE `marketing_user_template_getsingle`(IN usertemplate_id int(11))
BEGIN
select * FROM 
(
select mut.id as usertemplate_id,mut.sales_userId,
ifnull(mut.NAME,mtd.name) as name,
ifnull(mut.template,mtd.template) as template,
mtt.`type` templatetype,
mtt.id templatetypeid
,mut.sending_date
,mut.sending_year
from marketing_user_template mut 
inner JOIN marketing_template_detail mtd ON mtd.id =mut.marketing_template_detailId 
left join marketing_template_type mtt on mtt.id =mtd.marketing_template_typeId  
where  mut.id=usertemplate_id
union
select mut.id as usertemplate_id,sales_userId,
ifnull(mut.NAME,mtd.name) as name,
ifnull(ifnull(mut.template,mtc.template),mtd.template)  as template,
mtt.`type` templatetype,
mtt.id as templatetypeid
,ifnull(ifnull(mtc.date,mut.sending_date),mtd.date) sending_date
,case when marketing_template_countryId is not null then
'' 
else mut.sending_year end sending_year
from marketing_user_template mut 
inner join marketing_template_country mtc on mtc.id =mut.marketing_template_countryId 
left JOIN marketing_template_detail mtd ON mtd.id =mtc.marketing_template_detailId 
left join marketing_template_type mtt on mtt.id =mtd.marketing_template_typeId
where mut.id =usertemplate_id
UNION 
select mut.id as usertemplate_id,
sales_userId,
mut.name as name,
mut.template as template,
'custom'  as templatetype,
'0' as templatetypeid
,mut.sending_date
,mut.sending_year
from marketing_user_template mut 
where mut.id =usertemplate_id
and mut.marketing_template_countryId is null and 
mut.marketing_template_detailId is null
)A
order by A.templatetypeid;

END;;

CREATE PROCEDURE `marketing_user_template_get_images`(IN `userid` int(11), IN `usertemplate_id` text)
BEGIN
select * FROM 
(
select mut.id as usertemplate_id,mut.sales_userId, TO_BASE64(COALESCE(load_file(CONCAT('/home/business1.app/data.business1.app/template/user/',mut.id,'.png')),
-- load_file(CONCAT('/home/business1.app/data.business1.app/template/country/',mtc.id,'.png')),
load_file(CONCAT('/home/business1.app/data.business1.app/template/detail/',mtd.id,'.png'))))
as image,
ifnull(mut.NAME,mtd.name) as name,
ifnull(mut.template,mtd.template) as template,
mtt.`type` templatetype,
mtt.id templatetypeid, mut.last_update_image
,mut.sending_date
,mut.sending_year
from marketing_user_template mut 
inner JOIN marketing_template_detail mtd ON mtd.id =mut.marketing_template_detailId 
left join marketing_template_type mtt on mtt.id =mtd.marketing_template_typeId  
where  
mut.sales_userId=userid
and FIND_IN_SET(mut.id, usertemplate_id) > 0
union
select mut.id as usertemplate_id,sales_userId,TO_BASE64(COALESCE(load_file(CONCAT('/home/business1.app/data.business1.app/template/user/',mut.id,'.png')),
load_file(CONCAT('/home/business1.app/data.business1.app/template/country/',mtc.id,'.png')),
load_file(CONCAT('/home/business1.app/data.business1.app/template/detail/',mtd.id,'.png'))))
as image,
ifnull(mut.NAME,mtd.name) as name,
ifnull(ifnull(mut.template,mtc.template),mtd.template)  as template,
mtt.`type` templatetype,
mtt.id as templatetypeid, mut.last_update_image
,ifnull(ifnull(mtc.date,mut.sending_date),mtd.date) sending_date
,case when marketing_template_countryId is not null then
'' 
else mut.sending_year end sending_year
from marketing_user_template mut 
inner join marketing_template_country mtc on mtc.id =mut.marketing_template_countryId 
left JOIN marketing_template_detail mtd ON mtd.id =mtc.marketing_template_detailId 
left join marketing_template_type mtt on mtt.id =mtd.marketing_template_typeId
where 
mut.sales_userId=userid
and FIND_IN_SET(mut.id, usertemplate_id) > 0
UNION 
select mut.id as usertemplate_id,sales_userId, TO_BASE64(COALESCE(load_file(CONCAT('/home/business1.app/data.business1.app/template/user/',mut.id,'.png'))
-- ,load_file(CONCAT('/home/business1.app/data.business1.app/template/country/',mtc.id,'.png')),
-- load_file(CONCAT('/home/business1.app/data.business1.app/template/detail/',mtd.image,'.png'))
))
as image,
mut.name as name,
mut.template as template,
'custom'  as templatetype,
'0' as templatetypeid, mut.last_update_image
,mut.sending_date
,mut.sending_year
from marketing_user_template mut 
where 
mut.sales_userId=userid
and FIND_IN_SET(mut.id, usertemplate_id) > 0
and mut.marketing_template_countryId is null and 
mut.marketing_template_detailId is null
)A
order by A.templatetypeid;

END;;

CREATE PROCEDURE `marketing_user_template_insert`(IN `userid` int(11), IN `nameval` varchar(50), IN `templateval` varchar(200), IN `sending_date` varchar(10), IN `sending_year` varchar(5), IN `add_customer` bit(1), IN `add_owner` bit(1), IN `add_business` bit(1), IN `send_text_with_image` bit(1))
BEGIN
	DECLARE checkcount int;
	Declare checkname varchar(50);
	set checkname='';
	
if sending_date='' then
set sending_date=NULL ;
end if;

if sending_year='' then 
set sending_year=NULL ;
end if;

select count(name) into checkcount FROM 
(

select mut.id as usertemplate_id,mut.sales_userId,
COALESCE (mut.NAME,mtd.name,translation_get(mad.common_translationId,mtd.common_languageId)) as name,
COALESCE(mut.template,mtd.template) as template,
mtt.`type` templatetype,
mtt.id templatetypeid
from marketing_user_template mut 
inner JOIN marketing_template_detail mtd ON mtd.id =mut.marketing_template_detailId 
left join marketing_template_type mtt on mtt.id =mtd.marketing_template_typeId 
left join marketing_advertisement_detail mad on mad.marketing_template_typeId = mtt.id and mad.`order` =mut.marketing_advertisement_detailId 
where sales_userId =userid
union
select mut.id as usertemplate_id,sales_userId,
COALESCE(mut.NAME,mtd.name,translation_get(mad.common_translationId,mtd.common_languageId)) as name,
COALESCE(mut.template,mtc.template,mtd.template)  as template,
mtt.`type` templatetype,
mtt.id as templatetypeid
from marketing_user_template mut 
inner join marketing_template_country mtc on mtc.id =mut.marketing_template_countryId 
left JOIN marketing_template_detail mtd ON mtd.id =mtc.marketing_template_detailId 
left join marketing_template_type mtt on mtt.id =mtd.marketing_template_typeId
left join marketing_advertisement_detail mad on mad.marketing_template_typeId = mtt.id and mad.`order` =mut.marketing_advertisement_detailId 
where sales_userId =userid
UNION 
select mut.id as usertemplate_id,
sales_userId,
mut.name as name,
mut.template as template,
'custom'  as templatetype,
'0' as templatetypeid
from marketing_user_template mut 
where sales_userId =userid
and mut.marketing_template_countryId is null and 
mut.marketing_template_detailId is null
)A
where A.templatetypeid = 0;


select name into checkname from 
(
select distinct name FROM 
(

select mut.id as usertemplate_id,mut.sales_userId,
COALESCE (mut.NAME,mtd.name,translation_get(mad.common_translationId,mtd.common_languageId)) as name,
COALESCE(mut.template,mtd.template) as template,
mtt.`type` templatetype,
mtt.id templatetypeid
from marketing_user_template mut 
inner JOIN marketing_template_detail mtd ON mtd.id =mut.marketing_template_detailId 
left join marketing_template_type mtt on mtt.id =mtd.marketing_template_typeId
left join marketing_advertisement_detail mad on mad.marketing_template_typeId = mtt.id and mad.`order` =mut.marketing_advertisement_detailId 
where sales_userId =userid
union
select mut.id as usertemplate_id,sales_userId,
COALESCE(mut.NAME,mtd.name,translation_get(mad.common_translationId,mtd.common_languageId)) as name,
COALESCE(mut.template,mtc.template,mtd.template)  as template,
mtt.`type` templatetype,
mtt.id as templatetypeid
from marketing_user_template mut 
inner join marketing_template_country mtc on mtc.id =mut.marketing_template_countryId 
left JOIN marketing_template_detail mtd ON mtd.id =mtc.marketing_template_detailId 
left join marketing_template_type mtt on mtt.id =mtd.marketing_template_typeId
left join marketing_advertisement_detail mad on mad.marketing_template_typeId = mtt.id and mad.`order` =mut.marketing_advertisement_detailId 
where sales_userId =userid
UNION 
select mut.id as usertemplate_id,
sales_userId,
mut.name as name,
mut.template as template,
'custom'  as templatetype,
'0' as templatetypeid
from marketing_user_template mut 
where sales_userId =userid
and mut.marketing_template_countryId is null and 
mut.marketing_template_detailId is null
)A
where replace(lower(trim(A.name)),' ','')= replace(lower(trim(nameval)),' ','')
)B;

if (checkcount<=50 && checkname='') then
insert into marketing_user_template (sales_userId,name,template,sending_date,sending_year,add_customer,add_owner,add_business,send_text_with_image)
values (userid,nameval,templateval,sending_date,sending_year,add_customer,add_owner,add_business,send_text_with_image);
SELECT @@identity as result ;
elseif (checkname !='') then
select 'exist_template_name|usertemplate_id' as result ;
else
select 'maximum_custom_message_reached|userid' as result ;
end if;
END;;

CREATE PROCEDURE `marketing_user_template_update`(IN `usertemplate_id` int(11), IN `userid` int(11), IN `nameval` varchar(20), IN `templateval` varchar(200), IN `dateval` varchar(10), IN `yrval` varchar(5), IN `add_customer` bit(1), IN `add_owner` bit(1), IN `add_business` bit(1), IN `send_text_with_image` bit(1))
BEGIN
	
	DECLARE checkdetail varchar(10);
	Declare checkcountry varchar(10);
	Declare checktemplatename varchar(50);

	Declare checkname varchar(50);
	DECLARE checkvar int;

	set checkname='';

	set checkdetail=null;
	set checkcountry=null;
	set checktemplatename ='';
	
	if dateval='' then
	set dateval=null;
	end if;

	if yrval='' then
	set yrval=null;
	end if;
	
	
	
-- 	Validate usertemplate_id parameter
	If `usertemplate_id` is null or `usertemplate_id` < 1 THEN
		select 'id_greater_zero|usertemplate_id' as result;
	End if;
	
-- 	Validate userid parameter
	If `userid` is null or `userid` < 1 THEN
		select 'id_greater_zero|userid' as result;
	End if;
	
	
	
-- 	Validate nameval parameter
	SELECT nameval REGEXP '[a-d]' into checkvar;
	if checkvar is null or checkvar < 1 then
		select 'wrong_name_format|nameval' as result;
	end if;
	
	
--  Validate `dateval` parameter
	if (dateval is not null and yrval is not null) then
		if (select DAYNAME(concat(concat(`yrval`, '-'),`dateval`))) is null then
			select 'date_wrong_format|yrval|dateval' as result;
		end if;
	end if;


		if (dateval is null and yrval is not null) then
		select 'date_wrong_format|yrval|dateval' as result;
		elseif(nameval is null) then
		select 'template_name_unique|nameval' as result;
		elseif(templateval is null) then
		select 'template_empty|templateval' as result;
		else

		select marketing_template_detailId,
		marketing_template_countryId,
		name
		into checkdetail,checkcountry,checktemplatename
		from marketing_user_template mut 
		where id=usertemplate_id;

		
				if ((checkdetail is null and checkcountry is null) and checktemplatename != nameval) then 
		
				
		
						select name into checkname from 
								(select distinct name FROM 
											(
															
															   	select mut.id as usertemplate_id,mut.sales_userId,
															   	COALESCE (mut.NAME,mtd.name,translation_get(mad.common_translationId,mtd.common_languageId)) as name,
																COALESCE(mut.template,mtd.template) as template,
															   	mtt.`type` templatetype,
																mtt.id templatetypeid
																from marketing_user_template mut 
																inner JOIN marketing_template_detail mtd ON mtd.id =mut.marketing_template_detailId 
																left join marketing_template_type mtt on mtt.id =mtd.marketing_template_typeId  
																left join marketing_advertisement_detail mad on mad.marketing_template_typeId = mtt.id and mad.`order` =mut.marketing_advertisement_detailId 
																where sales_userId =userid
																union
																select mut.id as usertemplate_id,sales_userId,
																COALESCE(mut.NAME,mtd.name,translation_get(mad.common_translationId,mtd.common_languageId)) as name,
																COALESCE(mut.template,mtc.template,mtd.template)  as template,
																mtt.`type` templatetype,
																mtt.id as templatetypeid
																from marketing_user_template mut 
																inner join marketing_template_country mtc on mtc.id =mut.marketing_template_countryId 
																left JOIN marketing_template_detail mtd ON mtd.id =mtc.marketing_template_detailId 
																left join marketing_template_type mtt on mtt.id =mtd.marketing_template_typeId
																left join marketing_advertisement_detail mad on mad.marketing_template_typeId = mtt.id and mad.`order` =mut.marketing_advertisement_detailId 
																where sales_userId =userid
																UNION 
																select mut.id as usertemplate_id,
																sales_userId,
																mut.name as name,
																mut.template as template,
																'custom'  as templatetype,
																'0' as templatetypeid
																from marketing_user_template mut 
																where sales_userId =userid
																and mut.marketing_template_countryId is null and 
																mut.marketing_template_detailId is null
																)A
																where replace(lower(trim(A.name)),' ','')= replace(lower(trim(nameval)),' ','')
																)B;
								
								if checkname='' then 
update marketing_user_template set name=nameval,template =templateval,sending_date=dateval,sending_year =yrval, add_customer=add_customer, add_owner=add_owner, add_business=add_business,send_text_with_image=send_text_with_image 
								where id=usertemplate_id;
							
								select usertemplate_id as result;
								else
								select 'exist_template_name|usertemplate_id' as result ;
								end if;
							
			
			Elseif (checkdetail is not null or checkcountry is not null) and checktemplatename != nameval 
			then
			select  'template_standard_name_update|nameval' as result;
				else
				
				update marketing_user_template set template =templateval,sending_date=dateval,
				sending_year =yrval,
				add_customer=add_customer,
				add_owner=add_owner, add_business=add_business,send_text_with_image=send_text_with_image
				where id=usertemplate_id;
				select usertemplate_id as result;
				end if;
	end if;
END;;

CREATE PROCEDURE `refresh_token_by_token`(IN `token_pram` tinytext)
BEGIN
SELECT
    id,
    userId,
    token,
    expires,
    created,
    createdByIp,
    revoked,
    revokedByIp,
    replacedByToken,
    resonRevoked
FROM
	common_refresh_token
WHERE
	token= token_pram;
END;;

CREATE PROCEDURE `refresh_token_by_userId`(IN `userId_pram` int(11))
BEGIN
SELECT
    id,
    userId,
    token,
    expires,
    created,
    createdByIp,
    revoked,
    revokedByIp,
    replacedByToken,
    resonRevoked
FROM
	common_refresh_token
WHERE
	userId = userId_pram;
END;;

CREATE PROCEDURE `refresh_token_by_userId_and_connId`(IN `userId_pram` int(11), IN `connId_pram` varchar(50))
BEGIN
SELECT
    id,
    userId,
    token,
    expires,
    created,
    createdByIp,
    revoked,
    revokedByIp,
    replacedByToken,
    resonRevoked,
    connectionId,
    appSourceId
FROM
	common_refresh_token
WHERE
	userId = userId_pram and connectionId = connId_pram;
END;;

CREATE PROCEDURE `refresh_token_delete`(IN `id_pram` int(11))
BEGIN
DELETE FROM common_refresh_token
WHERE	
	id = id_pram;
END;;

CREATE PROCEDURE `refresh_token_insert`(IN `userId_pram` int(11), IN `token_pram` tinytext, IN `expires_pram` datetime, IN `created_pram` datetime, IN `createdByIp_pram` varchar(45), IN `revoked_pram` datetime, IN `revokedByIp_pram` datetime, IN `replacedByToken_pram` tinytext, IN `resonRevoked_pram` varchar(10), IN `app_source_id_pram` tinyint(1), IN `connection_id_pram` varchar(50))
BEGIN
INSERT INTO common_refresh_token(
    userId,
    token,
    expires,
    created,
    createdByIp,
    revoked,
    revokedByIp,
    replacedByToken,
    resonRevoked,
    connectionId,
    appSourceId
) VALUES (
    userId_pram,
    token_pram,
    expires_pram,
    created_pram,
    createdByIp_pram,
    revoked_pram,
    revokedByIp_pram,
    replacedByToken_pram,
    resonRevoked_pram,
    connection_id_pram,
    app_source_id_pram
);

SET @id = @@IDENTITY;

SELECT @@IDENTITY;

END;;

CREATE PROCEDURE `refresh_token_update`(IN `userId_pram` int(11), IN `id_pram` int(11), IN `token_pram` tinytext, IN `expires_pram` datetime, IN `created_pram` datetime, IN `createdByIp_pram` varchar(45), IN `revoked_pram` datetime, IN `revokedByIp_pram` datetime, IN `replacedByToken_pram` tinytext, IN `resonRevoked_pram` tinytext, IN `app_source_id_pram` tinyint, IN `connection_id_pram` varchar(50))
BEGIN
UPDATE common_refresh_token SET

    userId = userId_pram,
    token = token_pram,
    expires = expires_pram,
    created = created_pram,
    createdByIp = createdByIp_pram,
    revoked = revoked_pram,
    revokedByIp = revokedByIp_pram,
    replacedByToken = replacedByToken_pram,
    resonRevoked = resonRevoked_pram,
    connectionId = connection_id_pram,
    appSourceId = app_source_id_pram

WHERE
	id= id_pram;
END;;

CREATE PROCEDURE `Register`(IN `phone_param` varchar(15), IN `hash_pass_param` varchar(250) CHARACTER SET 'utf8', IN `salt_param` blob, IN `agent_userId_param` int(11), IN `name_param` varchar(20), IN `phone_countryId_param` tinyint(3), IN `centerId_param` smallint)
BEGIN
INSERT INTO sales_user(
phone,
hash_password,salt, agent_userId,phone_countryId,name,sales_centerId) 
VALUES (phone_param, hash_pass_param, salt_param, agent_userId_param, phone_countryId_param, name_param, centerId_param
);

SET @id = @@IDENTITY;

SELECT @@IDENTITY;

END;;

CREATE PROCEDURE `SalesUserInsert`(IN `ADDED_ON` datetime, IN `SALES_PLAN_ID` int(11), IN `PLAN_START_DATE` datetime, IN `BLOCKED` enum('yes'), IN `OTP` varchar(200), IN `OTP_TIME` datetime, IN `OTP_COUNT` int(1), IN `TOKEN` varchar(20), IN `PAYMENT_MODE` varchar(20), IN `hiring_source` enum('online','insurance'), IN `LAST_CALL` datetime, IN `NEXT_CALL` datetime, IN `VISITED` datetime, IN `VISIT_PURPOSE` enum('Sales','Renewal','New_device_installation','Troubleshoot'), IN `PHONE` varchar(15), IN `COUNTRYID` int(11), IN `NAME` varchar(20), IN `SALES_USER_REFERRED_BY_ID` int(11), IN `SALES_USER_AGENT_ID` int(11), IN `SALES_CENTER_NAME_ID` int(11), IN `DETAILS` varchar(200))
BEGIN
INSERT INTO sales_user(
phone,
countryId,
name,
sales_user_referred_byId,	
last_call,
next_call,
visited,	
visit_purpose,	
details,	
sales_user_agentId,
added_on,
sales_planId,
plan_start_date,	
blocked,	
otp,
otp_time,
otp_count,
token,
payment_mode,
sales_centerId,
hiring) 
VALUES (
    PHONE,
COUNTRYID,
NAME,
SALES_USER_REFERRED_BY_ID,	
LAST_CALL,
NEXT_CALL,
VISITED,	
VISIT_PURPOSE,	
DETAILS,	
SALES_USER_AGENT_ID,
ADDED_ON,
SALES_PLAN_ID,
PLAN_START_DATE,	
BLOCKED,	
OTP,
OTP_TIME,
OTP_COUNT,
TOKEN,
PAYMENT_MODE,
sales_centerId,
hiring_source
);

SET @id = @@IDENTITY;

SELECT @@IDENTITY;

END;;

CREATE PROCEDURE `sales_calendar_tasks`(
	IN `salescenteruserid` int,
	IN `salesusertype` int
)
BEGIN
	
	if salesusertype =1 then
		
		SELECT scd.sales_center_manager_id,
		scu.name as manager_name,
		scd.sales_candidateId,
		sc.id as candidate_id,
		sc.candidate_name,
		sc.phone,
		sc.candidate_status,
		sc.interview_status,
		sc.interview_date,
		sc.training_date,
		sc.applied_on,
		sut.designation,
		communication_date,
		communication_typeId,
		sct.name as communication_status,
		communication_description as task_description,
		task_date,
		DATE_FORMAT(task_date, "%d %b %Y | %h:%i %p") AS formate_task_date
		FROM sales_communication_detail scd 
			LEFT join sales_center_user scu on scu.id =scd.sales_center_manager_id 
			LEFT join sales_candidate sc on sc.id = scd.sales_candidateId 
			LEFT join sales_communication_type sct on sct.id =scd.communication_typeId 
			LEFT JOIN sales_user_type sut ON sut.id = sc.sales_user_type_id
				WHERE sales_center_manager_id =salescenteruserid;
	
	ELSE 
	
		SELECT scd.sales_center_manager_id,
		scu.name as manager_name,
		scd.sales_candidateId,
		sc.id as candidate_id,
		sc.candidate_name,
		sc.phone,
		sc.candidate_status,
		sc.interview_status,
		sc.interview_date,
		sc.training_date,
		sc.applied_on,
		sut.designation,
		communication_date,
		communication_typeId,
		sct.name as communication_status,
		communication_description as task_description,
		task_date,
		DATE_FORMAT(task_date, "%d %b %Y | %h:%i %p") AS formate_task_date
		FROM sales_communication_detail scd 
			LEFT join sales_center_user scu on scu.id =scd.sales_center_manager_id 
			LEFT join sales_candidate sc on sc.id = scd.sales_candidateId 
			LEFT join sales_communication_type sct on sct.id =scd.communication_typeId 
			LEFT JOIN sales_user_type sut ON sut.id = sc.sales_user_type_id
				WHERE sales_center_caller_id  = salescenteruserid;
	
	end if;

END;;

CREATE PROCEDURE `sales_candidate_list`()
BEGIN
	
SELECT 
sc.id,
ct.english AS countryname,
sc.candidate_name ,
sc.phone ,
sc.email,
sc.sales_centerId,
sc.confirm_token,
sc.confirm_status,
sc2.center_name ,
applied_on ,
candidate_status ,
interview_status ,
interview_date ,
training_date,
sales_user_type_id,
sales_center_userId,
status
FROM sales_candidate sc 
LEFT JOIN common_country cc ON cc.common_translationId = sc.phone_countryId
LEFT JOIN common_translation ct ON ct.id = cc.common_translationId 
LEFT JOIN sales_center sc2 ON sc2.id =sc.sales_centerId;

END;;

CREATE PROCEDURE `sales_candidate_list_with_id`(
	IN `sales_center_userId` INT
)
BEGIN

select 
sc.id,
cc.name as countryname,
sc.candidate_name ,
sc.phone ,
sc.sales_centerId,
sc2.center_name ,
applied_on ,
candidate_status ,
interview_status ,
interview_date ,
training_date,
sales_user_type_id,
sales_center_userId,
status,
scu.name AS center_user_name,
sct.designation,
sca.center_name AS assign_center_name

from sales_candidate sc 
left join common_country cc on cc.id =sc.common_countryId 
left join sales_center sc2 on sc2.id =sc.sales_centerId
LEFT JOIN sales_center  sca on sc.sales_assign_centerId=sca.id
LEFT JOIN sales_center_user  scu on sc.sales_center_userId=scu.id
LEFT JOIN sales_user_type sct ON sct.id = sc.sales_user_type_id
WHERE sc.sales_center_userId=sales_center_userId;

END;;

CREATE PROCEDURE `sales_candidate_with_id`(
	IN `candidate_id` INT
)
BEGIN
	
SELECT
sc.*,
ct.english as countryname,
sc2.center_name ,
scu.name AS center_user_name,
sct.designation,
sca.center_name AS assign_center_name
FROM sales_candidate AS sc 
	LEFT JOIN common_country AS cc ON cc.common_translationId = sc.phone_countryId 
	LEFT JOIN common_translation AS ct ON ct.id = cc.common_translationId 
	LEFT JOIN sales_center AS sc2 ON sc2.id = sc.sales_centerId
	LEFT JOIN sales_center AS sca ON sc.sales_assign_centerId = sca.id
	LEFT JOIN sales_center_user AS scu ON sc.sales_center_userId = scu.id
	LEFT JOIN sales_user_type AS sct ON sct.id = sc.sales_user_type_id
		WHERE sc.id=candidate_id;

END;;

CREATE PROCEDURE `sales_center_list`()
BEGIN

/*	SELECT sc.*, sc2.center_name AS main_center, cc.name AS country
	FROM sales_center AS sc 
		LEFT JOIN sales_center AS sc2 
			ON sc2.id = sc.main_centerId
		LEFT JOIN common_country AS cc
			ON cc.id = sc.common_countryId;*/
			

	SELECT sc.*, sc2.center_name AS main_center, ct.english AS country
	FROM sales_center AS sc 
	LEFT JOIN sales_center AS sc2 ON sc2.id = sc.main_centerId
	LEFT JOIN common_translation AS ct ON ct.id = sc.common_countryId;

END;;

CREATE PROCEDURE `sales_center_with_country`(
	IN `countryid` INT
)
BEGIN

	SELECT sc.id, sc.center_name FROM sales_center AS sc WHERE sc.common_countryId = countryid;

END;;

CREATE PROCEDURE `sales_main_center_list`()
BEGIN

SELECT * FROM sales_center WHERE id = main_centerId AND ACTIVE = 1;

END;;

CREATE PROCEDURE `sales_user_and_his_tokens_by_userId`(IN `userId_pram` int(11))
BEGIN 
SELECT
*,
(select 
concat('[', group_concat(json_object("id",id,"userId",userId,"token",token,"expires",expires,"created",created,"createdByIp",createdByIp,"revoked",revoked,"revokedByIp",revokedByIp,"replacedByToken",replacedByToken,"resonRevoked",resonRevoked,"connectionId",connectionId,"appSourceId",appSourceId)) ,']')
from common_refresh_token 
where userId = userId_pram) as rTokens
from sales_user as su where su.id = userId_pram;
end;;

CREATE PROCEDURE `sales_user_by_id`(IN `id_pram` int(11))
BEGIN
SELECT
*
FROM sales_user
WHERE id= id_pram;
END;;

CREATE PROCEDURE `sales_user_by_phone_countryId_otp`(IN `countryId_pram` int(11), IN `phone_pram` varchar(15))
BEGIN
SELECT
*
FROM sales_user
WHERE phone_countryId= countryId_pram and phone= phone_pram;
END;;

CREATE PROCEDURE `sales_user_by_refresh_token`(IN `token_pram` text)
BEGIN 
DECLARE userIdVal INT unsigned DEFAULT 0;
select userId from common_refresh_token where token = token_pram into userIdVal;

SELECT
*,
(select 
concat('[', group_concat(json_object("id",id,"userId",userId,"token",token,"expires",expires,"created",created,"createdByIp",createdByIp,"revoked",revoked,"revokedByIp",revokedByIp,"replacedByToken",replacedByToken,"resonRevoked",resonRevoked,"connectionId",connectionId,"appSourceId",appSourceId)) ,']')
from common_refresh_token 
where userId = userIdVal) as rTokens
from sales_user as su where su.id = userIdVal ;
end;;

CREATE PROCEDURE `sales_user_data_by_id`(
	IN `userid` INT
)
BEGIN

	SELECT scu.*, sc.center_name AS main_center, sc1.center_name AS center_1, sc2.center_name AS center_2,
	sc3.center_name AS center_3, sc.center_address, cc.name AS country_name, sut.designation AS designation, sr.role_name
	FROM sales_center_user AS scu
		LEFT JOIN sales_center AS sc ON sc.id = scu.sales_centerId
		LEFT JOIN sales_center AS sc1 ON sc1.id = scu.sales_center_1_Id
		LEFT JOIN sales_center AS sc2 ON sc2.id = scu.sales_center_2_Id
		LEFT JOIN sales_center AS sc3 ON sc3.id = scu.sales_center_3_Id
		LEFT JOIN common_country AS cc ON cc.id = sc.common_countryId
		LEFT JOIN sales_user_type AS sut ON sut.id = scu.sales_user_typeId
		LEFT JOIN sales_role AS sr ON sr.id = scu.sales_roleId
	WHERE scu.id = userid;

END;;

CREATE PROCEDURE `sales_user_plan_getall`(IN `in_sales_userId` int unsigned)
proc_label: BEGIN
DECLARE v_phone_countryId SMALLINT(5);
DECLARE v_date_blocked DATE;
DECLARE v_latest_plan_date DATE;
DECLARE v_calc_date DATE;
DECLARE has_plan INT DEFAULT 0;
DECLARE v_common_CID INT DEFAULT 0;
DECLARE v_country_planId INT DEFAULT 0;
DECLARE usr_plan_duration INT DEFAULT 0;
DECLARE v_plan_starting INT DEFAULT 0;
DECLARE v_new_date DATE;
DECLARE v_sales_planDId INT DEFAULT 0;
DECLARE v_plan_detailId INT DEFAULT 0;
DECLARE v_last_plan_seq INT DEFAULT 0;
DECLARE v_count_user INT DEFAULT 0;

SELECT phone_countryId, date_blocked INTO v_phone_countryId, v_date_blocked
FROM sales_user
WHERE id = in_sales_userId;

SELECT COUNT(*) INTO v_count_user FROM sales_user WHERE id = in_sales_userId;

IF v_count_user = 0 THEN
SELECT 'User does not exists' AS message;
LEAVE proc_label;
END IF;

SELECT COUNT(*) INTO v_common_CID FROM sales_plan_country WHERE common_countryId = v_phone_countryId;
-- Condition 1
IF v_common_CID = 0 THEN
SELECT 'Country not supported' AS message;
LEAVE proc_label;
END IF;
-- Condition 2
IF v_date_blocked IS NOT NULL THEN
SELECT 'User is blocked.' AS message;
LEAVE proc_label; 
END IF;

-- Condition 3
SELECT COUNT(*) INTO has_plan FROM sales_user_plan WHERE sales_userId = in_sales_userId;

IF has_plan = 0 THEN
SELECT spcd.id, spcd.amount, spcd.common_countryId, spd.plan_duration, spd.recurring, spd.plan_priority, spd.plan_starting
FROM sales_plan_country spcd
INNER JOIN sales_plan_detail spd ON spcd.plan_detailId = spd.id
WHERE spcd.common_countryId = v_phone_countryId AND spd.plan_starting = 1
ORDER BY spd.plan_priority;  

-- select 'User not having plan yet' AS message;
LEAVE proc_label; 
 END IF;
 
IF has_plan <> 0 THEN
	SELECT `plan_countryId`, plan_start INTO  v_country_planId, v_latest_plan_date FROM `sales_user_plan` 
	WHERE `sales_userId` = in_sales_userId ORDER BY `plan_start` DESC LIMIT 1;
	SELECT plan_detailId  INTO v_plan_detailId FROM `sales_plan_country` 
	WHERE `id` = v_country_planId;
	-- Get Plan_duration and plan starting for user 
	SELECT plan_duration, plan_starting INTO usr_plan_duration, v_plan_starting
	FROM `sales_plan_detail` WHERE `id` = v_plan_detailId;
	SET v_last_plan_seq = v_plan_detailId;
	SET v_new_date = DATE_SUB(DATE_ADD(DATE_ADD(v_latest_plan_date, INTERVAL usr_plan_duration MONTH), INTERVAL 1 MONTH), INTERVAL 1 DAY);

	-- select v_plan_detailId, usr_plan_duration, v_plan_starting, v_last_plan_seq, v_new_date, v_latest_plan_date,v_country_planId ;

	IF usr_plan_duration = 3 AND v_plan_starting = 1 THEN
		IF v_new_date <= CURDATE() THEN
		SELECT spcd.id, spcd.amount, spcd.common_countryId, spd.plan_duration, spd.recurring, spd.plan_priority, spd.plan_starting
		FROM sales_plan_country spcd
		INNER JOIN sales_plan_detail spd ON spcd.plan_detailId = spd.id
		WHERE spcd.plan_detailId IN (
			SELECT spd.id 
			FROM sales_plan_detail spd
			WHERE spd.plan_duration = '12'
			AND spd.discount = 1
			AND spd.id IN (
			SELECT sps.next_plan_detailId 
			FROM sales_plan_sequence sps
			WHERE sps.last_plan_detailId = v_last_plan_seq
			)
		) AND spcd.common_countryId = v_phone_countryId
		ORDER BY spd.plan_priority;
		ELSE
		SELECT spcd.id, spcd.amount, spcd.common_countryId, spd.plan_duration, spd.recurring, spd.plan_priority, spd.plan_starting
		FROM sales_plan_country spcd
		INNER JOIN sales_plan_detail spd ON spcd.plan_detailId = spd.id
		WHERE spcd.plan_detailId IN (
			SELECT spd.id 
			FROM sales_plan_detail spd
			WHERE spd.plan_duration = '12'
			AND spd.discount = 0
			AND spd.id IN (
			SELECT sps.next_plan_detailId 
			FROM sales_plan_sequence sps
			WHERE sps.last_plan_detailId = v_last_plan_seq
			)
		) AND spcd.common_countryId = v_phone_countryId
		ORDER BY spd.plan_priority;
		-- SELECT 'Existing User' AS message;
		LEAVE proc_label; 
		END IF;
	ELSE
	SELECT spcd.id, spcd.amount, spcd.common_countryId, spd.plan_duration, spd.recurring, spd.plan_priority, spd.plan_starting
		FROM sales_plan_country spcd
		INNER JOIN sales_plan_detail spd ON spcd.plan_detailId = spd.id
		WHERE spcd.plan_detailId IN (SELECT spd.id 
			FROM sales_plan_detail spd
			WHERE spd.id IN (
		SELECT sps.next_plan_detailId 
		FROM sales_plan_sequence sps
		WHERE sps.last_plan_detailId = v_last_plan_seq)) AND spcd.common_countryId = v_phone_countryId
		ORDER BY spd.plan_priority;
		-- SELECT 'Existing User' AS message;
		LEAVE proc_label; 
	END IF;
END IF;
END;;

CREATE PROCEDURE `settingGetByUserID`(userID int)
BEGIN
SELECT
	settingID
	userID,
	settings_nameID,
	value
FROM
	setting
WHERE
	userID = userID;
END;;

CREATE PROCEDURE `sp_last_update_customer`(IN `SALES_USER_ID_VAL` int(11))
BEGIN
UPDATE sales_user SET
    last_update_customer = NOW()
WHERE
	id = SALES_USER_ID_VAL;
END;;

CREATE PROCEDURE `sp_last_update_message`(IN `SALES_USER_ID_VAL` int(11))
BEGIN
UPDATE sales_user SET
    last_update_message = NOW()
WHERE
	id = SALES_USER_ID_VAL;
END;;

CREATE PROCEDURE `sp_last_update_setting`(IN `SALES_USER_ID_VAL` int(11))
BEGIN
UPDATE sales_user SET
    last_update_setting = NOW()
WHERE
	id = SALES_USER_ID_VAL;
END;;

CREATE PROCEDURE `sp_last_update_template`(IN `SALES_USER_ID_VAL` int(11))
BEGIN
UPDATE sales_user SET
    last_update_template = NOW()
WHERE
	id = SALES_USER_ID_VAL;
END;;

CREATE PROCEDURE `subscribe_user_to_country_templates`(IN `sales_userId` int, IN `marketing_template_countryId` int)
BEGIN

insert into marketing_user_template (
sales_userId,
marketing_template_countryId)
values (sales_userId,
 marketing_template_countryId);

SET @id = @@IDENTITY;

SELECT @@IDENTITY;        


END;;

CREATE PROCEDURE `update_sales_center_user`(
	IN `salesuserid` int
)
BEGIN
	declare getcountryval int;
	


	select common_countryId into getcountryval from sales_center sc 
	where id = (select sales_centerId from sales_center_user scu where id=salesuserid);

	
	select id,center_name as availablecenter from sales_center 
	where common_countryId=getcountryval and active=1
	and id not in (select * from (
				select sales_center_1_Id as assignedcenter  from sales_center_user where sales_centerId in 
				(select id from (select id from sales_center where common_countryId=getcountryval)A)
				UNION 
				select sales_center_2_Id as assignedcenter from sales_center_user where sales_centerId in 
				(select id from (select id from sales_center where common_countryId=getcountryval)B)
				UNION 
				select sales_center_3_Id as assignedcenter from sales_center_user where sales_centerId in 
				(select id from (select id from sales_center where common_countryId=getcountryval)C)
				)getcenters
				where assignedcenter is not null)
			
		union
	
		select id,center_name from sales_center sc 
		where id in 
		(
		select assignedcenter from 
				(
					select sales_center_1_Id as assignedcenter from sales_center_user where id=salesuserid
					UNION 
					select sales_center_2_Id as assignedcenter from sales_center_user where id=salesuserid
					union 
					select sales_center_3_Id as assignedcenter from sales_center_user where id=salesuserid
				)currentcenters
					where assignedcenter is not null
		);
		
		/* SELECT * FROM sales_center WHERE id IN (select secondary_sales_centerId from sales_center_user where id = salesuserid);*/
	
	
END;;

CREATE PROCEDURE `user_categories_with_templates`(IN `sales_userId` int, IN `category_detailId` int)
BEGIN

select listing_category_detailId,name,marketing_user_template_id,marketing_template_detail_id,assigned_to_user,template,TO_BASE64(image)  image
from
(SELECT luc.listing_category_detailId, lcd.name, mtd.id marketing_template_detail_id, mut.id marketing_user_template_id, COALESCE (mut.template,mtd.template ) template
,case when mut.id is null then 0 else 1 end assigned_to_user
, COALESCE(LOAD_FILE(concat('/home/business1.app/data.business1.app/template/user/',mut.id,'.png')),LOAD_FILE(concat('/home/business1.app/data.business1.app/template/detail/',mtd.id,'.png'))) image
FROM listing_user_category luc
INNER JOIN listing_category_detail lcd ON luc.listing_category_detailId = lcd.id
LEFT join marketing_template_detail mtd on luc.listing_category_detailId = mtd.listing_category_detailId
LEFT join marketing_user_template mut on mut.marketing_template_detailId =mtd.id and mut.sales_userId = luc.sales_userId 
WHERE luc.sales_userId = sales_userId and (luc.listing_category_detailId = category_detailId or category_detailId is null)
) a ;

END;;

CREATE PROCEDURE `user_template_insert`(IN `sales_userId` int, IN `marketing_template_countryId` tinyint)
BEGIN
INSERT INTO marketing_user_template(
	sales_userId,
	marketing_template_countryId
) VALUES (
	sales_userId,
	marketing_template_countryId
);
END;;

CREATE PROCEDURE `website_name_get_by_countryid`(
	IN `countryId` INT
)
BEGIN

SELECT wc.*, wn.name AS website_name, wn.duration  FROM sales_hiring_website_country AS wc
	LEFT JOIN sales_hiring_website_name AS wn
	ON wc.website_nameId = wn.id
	WHERE wc.common_countryId = countryId;

END;;

CREATE PROCEDURE `whatsappGetAll`()
BEGIN
SELECT
    a.id,
    a.user_id,
    b.name,
    b.phone,
    a.process_id,
    a.session_path,
    a.api_key,
    a.status
FROM `marketing_whatsapp` a
LEFT join `sales_user` b on b.id = a.user_id;
END;;

CREATE PROCEDURE `whatsappGetByUserId`(IN `ID_VAL` INT(11))
BEGIN
SELECT
    a.id,
    a.user_id,
    b.name,
    b.phone,
    a.process_id,
    a.session_path,
    a.api_key
FROM `marketing_whatsapp` a
LEFT join `sales_user` b on b.id = a.user_id
WHERE
	a.user_id = ID_VAL;
END;;

CREATE DEFINER=`username`@`%` PROCEDURE `whatsappInsert`(IN `USER_ID` INT(11), IN `API_KEY` TEXT, IN `SESSION_PATH` TEXT, IN `STATUS` VARCHAR(50), IN `PROCESS_ID` INT(11), IN `ACTIVE` TINYINT(3))
BEGIN
INSERT INTO `marketing_whatsapp`(
    `user_id`,
    `api_key`,
    `session_path`,
    `status`,
    `process_id`,
    `active`
) VALUES (
    USER_ID,
    API_KEY,
    SESSION_PATH,
    STATUS,
    PROCESS_ID,
    ACTIVE
);

SET @id = @@IDENTITY;

SELECT @@IDENTITY;

END;;

CREATE PROCEDURE `whatsappUpdate`(IN `ID_VAL` int(11), IN `USER_ID` INT(11), IN `API_KEY` TEXT, IN `SESSION_PATH` TEXT, IN `STATUS` VARCHAR(50), IN `PROCESS_ID` INT(11), IN `ACTIVE` TINYINT(3))
BEGIN
UPDATE `marketing_whatsapp` SET
    `user_id` = USER_ID,
    `api_key` = API_KEY,
    `session_path` = SESSION_PATH,
    `status` = STATUS,
    `process_id` = PROCESS_ID,
    `active` = ACTIVE
WHERE
	id= ID_VAL;
END;;

DELIMITER ;

DROP TABLE IF EXISTS `common_app`;
CREATE TABLE `common_app` (
  `common_translationId` smallint(5) unsigned NOT NULL,
  `login_phone` bit(1) NOT NULL,
  PRIMARY KEY (`common_translationId`),
  KEY `login_phone` (`login_phone`),
  CONSTRAINT `common_app_ibfk_4` FOREIGN KEY (`common_translationId`) REFERENCES `common_translation` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `common_country`;
CREATE TABLE `common_country` (
  `common_translationId` smallint(5) unsigned NOT NULL,
  `common_languageId` smallint(5) unsigned NOT NULL,
  `common_app_1Id` smallint(5) unsigned NOT NULL,
  `common_app_2Id` smallint(5) unsigned NOT NULL,
  `common_currencyId` tinyint(3) unsigned NOT NULL,
  `document` varchar(20) NOT NULL,
  `training_dayId` smallint(5) unsigned NOT NULL,
  `work_dayId` smallint(5) unsigned NOT NULL,
  `center_holidayId` smallint(5) unsigned NOT NULL,
  PRIMARY KEY (`common_translationId`),
  KEY `common_app_1Id` (`common_app_1Id`),
  KEY `common_app_2Id` (`common_app_2Id`),
  KEY `common_languageId` (`common_languageId`),
  KEY `common_currencyId` (`common_currencyId`),
  KEY `document` (`document`),
  KEY `training_dayId` (`training_dayId`),
  KEY `second_holidayId` (`work_dayId`),
  KEY `center_holidayId` (`center_holidayId`),
  CONSTRAINT `common_country_ibfk_19` FOREIGN KEY (`common_translationId`) REFERENCES `common_translation` (`id`) ON UPDATE CASCADE,
  CONSTRAINT `common_country_ibfk_20` FOREIGN KEY (`training_dayId`) REFERENCES `business_day` (`common_translationId`) ON UPDATE CASCADE,
  CONSTRAINT `common_country_ibfk_21` FOREIGN KEY (`common_languageId`) REFERENCES `common_language` (`common_translationId`) ON UPDATE CASCADE,
  CONSTRAINT `common_country_ibfk_22` FOREIGN KEY (`common_app_1Id`) REFERENCES `common_app` (`common_translationId`) ON UPDATE CASCADE,
  CONSTRAINT `common_country_ibfk_23` FOREIGN KEY (`common_app_2Id`) REFERENCES `common_app` (`common_translationId`) ON UPDATE CASCADE,
  CONSTRAINT `common_country_ibfk_24` FOREIGN KEY (`common_currencyId`) REFERENCES `common_currency` (`id`) ON UPDATE CASCADE,
  CONSTRAINT `common_country_ibfk_25` FOREIGN KEY (`work_dayId`) REFERENCES `business_day` (`common_translationId`) ON UPDATE CASCADE,
  CONSTRAINT `common_country_ibfk_26` FOREIGN KEY (`center_holidayId`) REFERENCES `business_day` (`common_translationId`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `common_country_timezone`;
CREATE TABLE `common_country_timezone` (
  `id` smallint(6) NOT NULL AUTO_INCREMENT,
  `common_countryId` smallint(5) unsigned NOT NULL,
  `common_timezoneId` smallint(5) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `common_countryId_common_timezoneId` (`common_countryId`,`common_timezoneId`),
  KEY `common_timezoneId` (`common_timezoneId`),
  CONSTRAINT `common_country_timezone_ibfk_4` FOREIGN KEY (`common_timezoneId`) REFERENCES `common_timezone` (`id`) ON UPDATE CASCADE,
  CONSTRAINT `common_country_timezone_ibfk_5` FOREIGN KEY (`common_countryId`) REFERENCES `common_country` (`common_translationId`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `common_currency`;
CREATE TABLE `common_currency` (
  `id` tinyint(3) unsigned NOT NULL AUTO_INCREMENT,
  `name` enum('AED','ALL','AMD','AOA','ARS','AUD','AZN','BAM','BDT','BGN','BHD','BND','BOB','BRL','BWP','BYN','CAD','CHF','CLP','CNY','COP','CRC','CVE','CZK','DJF','DKK','DOP','DZD','EGP','ETB','EUR','FJD','GBP','GEL','GHS','GTQ','GYD','HKD','HNL','HRK','HUF','IDR','ILS','INR','IQD','IRR','JMD','JOD','JPY','KES','KRW','KWD','KZT','LAK','LBP','LKR','LYD','MAD','MDL','MGA','MKD','MNT','MOP','MUR','MXN','MYR','NGN','NIO','NOK','NPR','NZD','OMR','PEN','PHP','PKR','PLN','PYG','QAR','RON','RSD','RUB','SAR','SEK','SGD','THB','TMT','TND','TRY','TTD','TWD','UAH','USD','UYU','UZS','VND','XAF','XOF','ZAR') NOT NULL,
  `sign` enum('֏','₸','₼','₽','₾','$','.د.ب','£','¥','₱','﷼','₭','₦','₨','₩','₮','€','฿','₡','৳','₲','₴','₪','₫','₹','₺','Ar','Br','Bs','C$','CFA','den','DH','din','FCFA','Fdj','FJ$','fr.','Ft','GH₵','Kč','KM','kn','kr','kr.','KSh','Kz','L','P','Q','R','R$','RM','Rp','S/','T','zł','лв','сум','د.إ','د.أ','د.ت','د.ع','د.ك','دج','ر.س','ر.ع.','ر.ق','ل.د','ل.ل.','रु') NOT NULL,
  `value` double unsigned NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `name` (`name`),
  KEY `sign` (`sign`),
  KEY `value` (`value`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `common_default`;
CREATE TABLE `common_default` (
  `id` tinyint(3) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `value` varchar(250) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `name` (`name`),
  KEY `value` (`value`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `common_error_name`;
CREATE TABLE `common_error_name` (
  `code` varchar(50) NOT NULL,
  `common_translationId` smallint(5) unsigned NOT NULL,
  PRIMARY KEY (`common_translationId`),
  UNIQUE KEY `code` (`code`),
  CONSTRAINT `common_error_name_ibfk_2` FOREIGN KEY (`common_translationId`) REFERENCES `common_translation` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `common_language`;
CREATE TABLE `common_language` (
  `common_translationId` smallint(5) unsigned NOT NULL,
  PRIMARY KEY (`common_translationId`),
  CONSTRAINT `common_language_ibfk_2` FOREIGN KEY (`common_translationId`) REFERENCES `common_translation` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `common_refresh_token`;
CREATE TABLE `common_refresh_token` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `userId` int(10) unsigned NOT NULL,
  `token` text NOT NULL,
  `expires` datetime NOT NULL,
  `created` datetime NOT NULL,
  `createdByIp` varchar(50) NOT NULL,
  `revoked` datetime DEFAULT NULL,
  `revokedByIp` varchar(50) DEFAULT NULL,
  `replacedByToken` tinytext DEFAULT NULL,
  `resonRevoked` tinytext DEFAULT NULL,
  `connectionId` varchar(50) DEFAULT NULL,
  `appSourceId` tinyint(1) unsigned DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `userId` (`userId`),
  CONSTRAINT `common_refresh_token_ibfk_1` FOREIGN KEY (`userId`) REFERENCES `sales_user` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `common_timezone`;
CREATE TABLE `common_timezone` (
  `id` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `name` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `common_time_schedule`;
CREATE TABLE `common_time_schedule` (
  `id` tinyint(3) unsigned NOT NULL AUTO_INCREMENT,
  `time` enum('10:00','10:30','11:00','11:30','12:00','12:30','13:00','13:30','14:00','14:30','15:00','15:30','16:00','16:30','17:00') NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `time` (`time`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `common_translation`;
CREATE TABLE `common_translation` (
  `id` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `english` varchar(100) NOT NULL,
  `arabic` varchar(100) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `english` (`english`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `listing_business_type`;
CREATE TABLE `listing_business_type` (
  `id` smallint(1) unsigned NOT NULL AUTO_INCREMENT,
  `type` enum('Manufacture / Export','Wholesale / Distributor / Importer','Retail / Shop / Showroom / Outlet','Service / Rent / Hire') NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `type` (`type`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `listing_category_detail`;
CREATE TABLE `listing_category_detail` (
  `id` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(150) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `name` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `listing_category_keyword`;
CREATE TABLE `listing_category_keyword` (
  `listing_category_detailId` smallint(5) unsigned NOT NULL,
  `common_translationId` smallint(5) unsigned NOT NULL,
  PRIMARY KEY (`common_translationId`,`listing_category_detailId`),
  KEY `fk_marketing_category_keywords_1` (`listing_category_detailId`),
  CONSTRAINT `listing_category_keyword_ibfk_4` FOREIGN KEY (`listing_category_detailId`) REFERENCES `listing_category_detail` (`id`) ON UPDATE CASCADE,
  CONSTRAINT `listing_category_keyword_ibfk_5` FOREIGN KEY (`common_translationId`) REFERENCES `common_translation` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `listing_category_main`;
CREATE TABLE `listing_category_main` (
  `listing_category_detailId` smallint(5) unsigned NOT NULL,
  `listing_category_typeId` bit(1) NOT NULL,
  PRIMARY KEY (`listing_category_detailId`),
  KEY `listing_category_typeId` (`listing_category_typeId`),
  CONSTRAINT `listing_category_main_ibfk_6` FOREIGN KEY (`listing_category_typeId`) REFERENCES `listing_category_type` (`id`) ON UPDATE CASCADE,
  CONSTRAINT `listing_category_main_ibfk_7` FOREIGN KEY (`listing_category_detailId`) REFERENCES `listing_category_detail` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `listing_category_relation`;
CREATE TABLE `listing_category_relation` (
  `listing_category_detailId` smallint(5) unsigned NOT NULL,
  `parent_listing_category_detailId` smallint(5) unsigned NOT NULL,
  PRIMARY KEY (`listing_category_detailId`,`parent_listing_category_detailId`),
  KEY `parent_listing_category_detailId` (`parent_listing_category_detailId`),
  CONSTRAINT `listing_category_relation_ibfk_3` FOREIGN KEY (`listing_category_detailId`) REFERENCES `listing_category_detail` (`id`) ON UPDATE CASCADE,
  CONSTRAINT `listing_category_relation_ibfk_4` FOREIGN KEY (`parent_listing_category_detailId`) REFERENCES `listing_category_detail` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `listing_category_type`;
CREATE TABLE `listing_category_type` (
  `id` bit(1) NOT NULL,
  `type` enum('Business / Industrial','Personal / Consumer') NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `type` (`type`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `listing_user_business`;
CREATE TABLE `listing_user_business` (
  `id` mediumint(8) unsigned NOT NULL AUTO_INCREMENT,
  `sales_userId` int(10) unsigned NOT NULL,
  `listing_business_typeId` smallint(5) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `sales_userId_listing_business_typeId` (`sales_userId`,`listing_business_typeId`),
  KEY `fk_marketing_user_business_type_1` (`sales_userId`),
  KEY `fk_marketing_user_business_type_2` (`listing_business_typeId`),
  CONSTRAINT `listing_user_business_ibfk_1` FOREIGN KEY (`listing_business_typeId`) REFERENCES `listing_business_type` (`id`) ON UPDATE CASCADE,
  CONSTRAINT `listing_user_business_ibfk_2` FOREIGN KEY (`sales_userId`) REFERENCES `sales_user` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `listing_user_category`;
CREATE TABLE `listing_user_category` (
  `sales_userId` int(10) unsigned NOT NULL,
  `listing_category_detailId` smallint(5) unsigned NOT NULL,
  PRIMARY KEY (`sales_userId`,`listing_category_detailId`),
  KEY `listing_category_detailId` (`listing_category_detailId`),
  CONSTRAINT `listing_user_category_ibfk_5` FOREIGN KEY (`sales_userId`) REFERENCES `sales_user` (`id`) ON UPDATE CASCADE,
  CONSTRAINT `listing_user_category_ibfk_6` FOREIGN KEY (`listing_category_detailId`) REFERENCES `listing_category_detail` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `listing_user_keyword`;
CREATE TABLE `listing_user_keyword` (
  `sales_userId` int(10) unsigned NOT NULL,
  `keyword` varchar(20) NOT NULL,
  PRIMARY KEY (`sales_userId`,`keyword`),
  CONSTRAINT `listing_user_keyword_ibfk_3` FOREIGN KEY (`sales_userId`) REFERENCES `sales_user` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `marketing_advertisement_detail`;
CREATE TABLE `marketing_advertisement_detail` (
  `marketing_template_typeId` tinyint(3) unsigned NOT NULL,
  `common_translationId` smallint(5) unsigned NOT NULL,
  `order` enum('1','2','3','4') NOT NULL,
  PRIMARY KEY (`order`),
  UNIQUE KEY `common_translationId` (`common_translationId`),
  KEY `marketing_template_type` (`marketing_template_typeId`),
  CONSTRAINT `marketing_advertisement_detail_ibfk_2` FOREIGN KEY (`marketing_template_typeId`) REFERENCES `marketing_template_type` (`id`) ON UPDATE CASCADE,
  CONSTRAINT `marketing_advertisement_detail_ibfk_3` FOREIGN KEY (`common_translationId`) REFERENCES `common_translation` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `marketing_template_country`;
CREATE TABLE `marketing_template_country` (
  `id` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `common_countryId` smallint(5) unsigned NOT NULL,
  `marketing_template_detailId` mediumint(8) unsigned NOT NULL,
  `template` varchar(120) DEFAULT NULL,
  `date` varchar(5) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `marketing_template_detailId_common_countryId` (`marketing_template_detailId`,`common_countryId`),
  KEY `marketing_template_detailId` (`marketing_template_detailId`),
  KEY `common_countryId` (`common_countryId`),
  KEY `date` (`date`),
  CONSTRAINT `marketing_template_country_ibfk_15` FOREIGN KEY (`common_countryId`) REFERENCES `common_country` (`common_translationId`) ON UPDATE CASCADE,
  CONSTRAINT `marketing_template_country_ibfk_16` FOREIGN KEY (`marketing_template_detailId`) REFERENCES `marketing_template_detail` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `marketing_template_date`;
CREATE TABLE `marketing_template_date` (
  `id` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `marketing_template_countryId` smallint(5) unsigned NOT NULL,
  `date` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `marketing_template_countryId_date` (`marketing_template_countryId`,`date`),
  CONSTRAINT `marketing_template_date_ibfk_2` FOREIGN KEY (`marketing_template_countryId`) REFERENCES `marketing_template_country` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `marketing_template_detail`;
CREATE TABLE `marketing_template_detail` (
  `id` mediumint(9) unsigned NOT NULL AUTO_INCREMENT,
  `marketing_template_typeId` tinyint(3) unsigned NOT NULL,
  `common_languageId` smallint(5) unsigned NOT NULL,
  `listing_category_detailId` smallint(5) unsigned DEFAULT NULL,
  `name` varchar(20) DEFAULT NULL,
  `template` varchar(120) NOT NULL,
  `date` varchar(5) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `marketing_template_typeId_common_languageId_name` (`marketing_template_typeId`,`common_languageId`,`name`),
  KEY `marketing_template_typeId_2` (`marketing_template_typeId`),
  KEY `marketing_industry_nameId` (`listing_category_detailId`),
  KEY `name` (`name`),
  KEY `common_languageId` (`common_languageId`),
  CONSTRAINT `marketing_template_detail_ibfk_10` FOREIGN KEY (`common_languageId`) REFERENCES `common_language` (`common_translationId`) ON UPDATE CASCADE,
  CONSTRAINT `marketing_template_detail_ibfk_7` FOREIGN KEY (`marketing_template_typeId`) REFERENCES `marketing_template_type` (`id`) ON UPDATE CASCADE,
  CONSTRAINT `marketing_template_detail_ibfk_9` FOREIGN KEY (`listing_category_detailId`) REFERENCES `listing_category_detail` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `marketing_template_type`;
CREATE TABLE `marketing_template_type` (
  `id` tinyint(3) unsigned NOT NULL AUTO_INCREMENT,
  `type` enum('advertisement','birthday','event','feedback','other','reminder') NOT NULL,
  `common_translationId` smallint(5) unsigned DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `type` (`type`),
  UNIQUE KEY `common_translationId` (`common_translationId`),
  CONSTRAINT `marketing_template_type_ibfk_1` FOREIGN KEY (`common_translationId`) REFERENCES `common_translation` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `marketing_user_customer`;
CREATE TABLE `marketing_user_customer` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `sales_userId` int(10) unsigned NOT NULL,
  `phone_countryId` smallint(5) unsigned NOT NULL,
  `phone` varchar(12) NOT NULL,
  `name` varchar(20) DEFAULT NULL,
  `phone_alternate` varchar(12) DEFAULT NULL,
  `email` varchar(50) DEFAULT NULL,
  `last_visit` date DEFAULT NULL,
  `added` datetime NOT NULL DEFAULT current_timestamp(),
  `birthday` varchar(5) DEFAULT NULL,
  `year` enum('1940','1941','1942','1943','1944','1945','1946','1947','1948','1949','1950','1951','1952','1953','1954','1955','1956','1957','1958','1959','1960','1961','1962','1963','1964','1965','1966','1967','1968','1969','1970','1971','1972','1973','1974','1975','1976','1977','1978','1979','1980','1981','1982','1983','1984','1985','1986','1987','1988','1989','1990','1991','1992','1993','1994','1995','1996','1997','1998','1999','2000','2001','2002','2003','2004','2005','2006','2007','2008','2009','2010','2011','2012','2013','2014','2015','2016','2017','2018','2019','2020','2021','2022','2023','2024','2025','2026','2027','2028','2029','2030','2031','2032','2033','2034','2035','2036','2037','2038','2039','2040','2041','2042','2043','2044','2045','2046','2047','2048','2049','2050','2051','2052','2053','2054','2055','2056','2057','2058','2059','2060','2061','2062','2063','2064','2065','2066','2067','2068','2069','2070','2071','2072','2073','2074','2075','2076','2077','2078','2079','2080','2081','2082','2083','2084','2085','2086','2087','2088','2089','2090','2091','2092','2093','2094','2095','2096','2097','2098','2099') DEFAULT NULL,
  `address` varchar(100) DEFAULT NULL,
  `details` varchar(100) DEFAULT NULL,
  `common_app_1Id` smallint(5) unsigned DEFAULT NULL,
  `common_app_2Id` smallint(5) unsigned DEFAULT NULL,
  `app_2_id` varchar(30) DEFAULT NULL,
  `send_events_msg` bit(1) DEFAULT NULL,
  `send_feedback_msg` bit(1) DEFAULT NULL,
  `send_advertise_msg` bit(1) DEFAULT NULL,
  `send_reminder_msg` bit(1) DEFAULT NULL,
  `reminder_duration` enum('1','3','6','12') CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_520_nopad_ci DEFAULT '3',
  `reminder_count` enum('0','1','2','3') NOT NULL DEFAULT '0',
  `last_reminder` date DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `sales_userId_common_countryId_phone` (`sales_userId`,`phone_countryId`,`phone`),
  KEY `messaging_app` (`common_app_1Id`),
  KEY `social_app` (`common_app_2Id`),
  KEY `country` (`phone_countryId`),
  KEY `phone` (`phone`),
  KEY `user` (`sales_userId`),
  CONSTRAINT `marketing_user_customer_ibfk_11` FOREIGN KEY (`phone_countryId`) REFERENCES `common_country` (`common_translationId`) ON UPDATE CASCADE,
  CONSTRAINT `marketing_user_customer_ibfk_12` FOREIGN KEY (`sales_userId`) REFERENCES `sales_user` (`id`) ON UPDATE CASCADE,
  CONSTRAINT `marketing_user_customer_ibfk_13` FOREIGN KEY (`common_app_1Id`) REFERENCES `common_app` (`common_translationId`) ON UPDATE CASCADE,
  CONSTRAINT `marketing_user_customer_ibfk_14` FOREIGN KEY (`common_app_2Id`) REFERENCES `common_app` (`common_translationId`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `marketing_user_message`;
CREATE TABLE `marketing_user_message` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `sales_userId` int(10) unsigned NOT NULL,
  `marketing_user_customerId` int(10) unsigned NOT NULL,
  `marketing_user_templateId` int(10) unsigned NOT NULL,
  `sent` bit(1) DEFAULT NULL,
  `datetime` datetime NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `userId_customerId_templateId_scheduled_time` (`sales_userId`,`marketing_user_customerId`,`marketing_user_templateId`,`datetime`),
  KEY `customer` (`marketing_user_customerId`),
  KEY `template` (`marketing_user_templateId`),
  KEY `userId` (`sales_userId`),
  CONSTRAINT `marketing_user_message_ibfk_19` FOREIGN KEY (`sales_userId`) REFERENCES `sales_user` (`id`) ON UPDATE CASCADE,
  CONSTRAINT `marketing_user_message_ibfk_20` FOREIGN KEY (`marketing_user_customerId`) REFERENCES `marketing_user_customer` (`id`) ON UPDATE CASCADE,
  CONSTRAINT `marketing_user_message_ibfk_21` FOREIGN KEY (`marketing_user_templateId`) REFERENCES `marketing_user_template` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `marketing_user_template`;
CREATE TABLE `marketing_user_template` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `sales_userId` int(10) unsigned NOT NULL,
  `marketing_template_detailId` mediumint(8) unsigned DEFAULT NULL,
  `marketing_template_countryId` smallint(5) unsigned DEFAULT NULL,
  `marketing_advertisement_detailId` tinyint(3) unsigned DEFAULT NULL,
  `name` varchar(20) DEFAULT NULL,
  `template` varchar(160) DEFAULT NULL,
  `sending_date` varchar(10) DEFAULT NULL,
  `sending_year` enum('2022','2023','2024','2025','2026','2027','2028','2029','2030','2031','2032','2033','2034','2035','2036','2037','2038','2039','2040','2041','2042','2043','2044','2045','2046','2047','2048','2049','2050','2051','2052','2053','2054','2055','2056','2057','2058','2059','2060','2061','2062','2063','2064','2065','2066','2067','2068','2069','2070','2071','2072','2073','2074','2075','2076','2077','2078','2079','2080','2081','2082','2083','2084','2085','2086','2087','2088','2089','2090','2091','2092','2093','2094','2095','2096','2097','2098','2099') DEFAULT NULL,
  `add_customer` bit(1) NOT NULL,
  `add_owner` bit(1) NOT NULL,
  `add_business` bit(1) NOT NULL,
  `send_text_with_image` bit(1) NOT NULL,
  `last_update_image` datetime(3) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `sales_userId` (`sales_userId`,`marketing_template_detailId`),
  UNIQUE KEY `sales_userId_marketing_country_eventId` (`sales_userId`,`marketing_template_countryId`),
  UNIQUE KEY `sales_userId_name` (`sales_userId`,`name`),
  UNIQUE KEY `sales_userId_marketing_advertisement_detailId` (`sales_userId`,`marketing_advertisement_detailId`),
  KEY `customer_customerfk` (`sales_userId`),
  KEY `customer_messagefk` (`marketing_template_detailId`),
  KEY `country_event` (`marketing_template_countryId`),
  KEY `name` (`name`),
  KEY `sending_date` (`sending_date`),
  KEY `sending_year` (`sending_year`),
  KEY `marketing_advertisement_detailId` (`marketing_advertisement_detailId`),
  CONSTRAINT `marketing_user_template_ibfk_17` FOREIGN KEY (`sales_userId`) REFERENCES `sales_user` (`id`) ON UPDATE CASCADE,
  CONSTRAINT `marketing_user_template_ibfk_18` FOREIGN KEY (`marketing_template_detailId`) REFERENCES `marketing_template_detail` (`id`) ON UPDATE CASCADE,
  CONSTRAINT `marketing_user_template_ibfk_19` FOREIGN KEY (`marketing_template_countryId`) REFERENCES `marketing_template_country` (`id`) ON UPDATE CASCADE,
  CONSTRAINT `marketing_user_template_ibfk_20` FOREIGN KEY (`marketing_advertisement_detailId`) REFERENCES `marketing_advertisement_detail` (`order`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `personal_access_tokens`;
CREATE TABLE `personal_access_tokens` (
  `id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `tokenable_type` varchar(191) NOT NULL,
  `tokenable_id` bigint(20) unsigned NOT NULL,
  `name` varchar(191) NOT NULL,
  `token` varchar(64) NOT NULL,
  `abilities` text DEFAULT NULL,
  `last_used_on` datetime DEFAULT NULL,
  `created_on` datetime DEFAULT NULL,
  `updated_on` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `personal_access_tokens_token_unique` (`token`),
  KEY `personal_access_tokens_tokenable_type_tokenable_id_index` (`tokenable_type`,`tokenable_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `sales_user`;
CREATE TABLE `sales_user` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `phone_countryId` smallint(5) unsigned NOT NULL,
  `phone` varchar(12) NOT NULL,
  `phone_alternate` varchar(12) DEFAULT NULL,
  `name` varchar(40) NOT NULL,
  `email` varchar(50) DEFAULT NULL,
  `referred_by_userId` int(10) unsigned DEFAULT NULL,
  `sales_agentId` int(10) unsigned DEFAULT NULL,
  `sales_centerId` smallint(5) unsigned DEFAULT NULL,
  `birthday` varchar(5) DEFAULT NULL,
  `otp` varchar(4) DEFAULT NULL,
  `otp_time` datetime DEFAULT NULL,
  `otp_count` tinyint(3) unsigned DEFAULT NULL,
  `token` varchar(20) DEFAULT NULL,
  `message_total` int(10) unsigned NOT NULL,
  `send_feedback` bit(1) DEFAULT NULL,
  `send_birthday` bit(1) DEFAULT NULL,
  `send_events` bit(1) DEFAULT NULL,
  `send_advertisement` bit(1) DEFAULT NULL,
  `send_reminders` bit(1) DEFAULT NULL,
  `reminder_times` enum('1','2','3') DEFAULT '3',
  `reminder_duration` enum('1','3','6','12') DEFAULT NULL,
  `delete_customer` bit(1) DEFAULT b'0',
  `message_time_before` enum('01','02','03','04','05','06','07','08','09','10','11','12','13','14','15','16','17','18','19','20','21','22','23','24') DEFAULT NULL,
  `message_time_after` enum('01','02','03','04','05','06','07','08','09','10','11','12','13','14','15','16','17','18','19','20','21','22','23','24') DEFAULT NULL,
  `common_app_1Id` smallint(5) unsigned DEFAULT NULL,
  `common_app_2Id` smallint(5) unsigned DEFAULT NULL,
  `contact_timeId` tinyint(3) unsigned DEFAULT NULL,
  `holiday_dayId` smallint(5) unsigned DEFAULT NULL,
  `user_timezoneId` smallint(5) unsigned DEFAULT NULL,
  `last_update_customer` datetime(3) DEFAULT NULL,
  `last_update_message` datetime(3) DEFAULT NULL,
  `last_update_setting` datetime(3) DEFAULT NULL,
  `last_update_template` datetime(3) DEFAULT NULL,
  `last_update_template_date` datetime(3) DEFAULT NULL,
  `hash_password` varchar(250) DEFAULT NULL,
  `salt` blob DEFAULT NULL,
  `pt` varchar(50) DEFAULT NULL,
  `win_con_id` varchar(50) DEFAULT NULL,
  `mob_con_id` varchar(50) DEFAULT NULL,
  `web_con_id` varchar(50) DEFAULT NULL,
  `date_blocked` date DEFAULT NULL,
  `date_added` date NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE KEY `phone` (`phone`,`phone_countryId`),
  UNIQUE KEY `email` (`email`),
  KEY `center` (`sales_centerId`),
  KEY `referred_by` (`referred_by_userId`),
  KEY `agent` (`sales_agentId`),
  KEY `phone_countryId` (`phone_countryId`),
  KEY `common_app_1Id` (`common_app_1Id`),
  KEY `common_app_2Id` (`common_app_2Id`),
  KEY `common_timezoneId` (`user_timezoneId`),
  KEY `contact_timeId` (`contact_timeId`),
  KEY `message_timeId` (`message_time_before`),
  KEY `holiday_dayId` (`holiday_dayId`),
  CONSTRAINT `sales_user_ibfk_63` FOREIGN KEY (`sales_centerId`) REFERENCES `sales_center_detail` (`id`) ON UPDATE CASCADE,
  CONSTRAINT `sales_user_ibfk_64` FOREIGN KEY (`referred_by_userId`) REFERENCES `sales_user` (`id`) ON UPDATE CASCADE,
  CONSTRAINT `sales_user_ibfk_65` FOREIGN KEY (`common_app_1Id`) REFERENCES `common_app` (`common_translationId`) ON UPDATE CASCADE,
  CONSTRAINT `sales_user_ibfk_67` FOREIGN KEY (`phone_countryId`) REFERENCES `common_country` (`common_translationId`) ON UPDATE CASCADE,
  CONSTRAINT `sales_user_ibfk_68` FOREIGN KEY (`common_app_2Id`) REFERENCES `common_app` (`common_translationId`) ON UPDATE CASCADE,
  CONSTRAINT `sales_user_ibfk_79` FOREIGN KEY (`contact_timeId`) REFERENCES `common_time_schedule` (`id`) ON UPDATE CASCADE,
  CONSTRAINT `sales_user_ibfk_80` FOREIGN KEY (`user_timezoneId`) REFERENCES `common_timezone` (`id`) ON UPDATE CASCADE,
  CONSTRAINT `sales_user_ibfk_81` FOREIGN KEY (`sales_agentId`) REFERENCES `sales_agent` (`sales_userId`) ON UPDATE CASCADE,
  CONSTRAINT `sales_user_ibfk_82` FOREIGN KEY (`holiday_dayId`) REFERENCES `business_day` (`common_translationId`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


DROP TABLE IF EXISTS `sales_user_device_detail`;
CREATE TABLE `sales_user_device_detail` (
  `id` int(10) unsigned NOT NULL,
  `sales_userId` int(10) unsigned NOT NULL,
  `connection` varchar(50) NOT NULL,
  `device_type` enum('web','windows','mobile','win-automation','chrome-extention') NOT NULL,
  `last_login` datetime(3) NOT NULL,
  KEY `sales_userId` (`sales_userId`),
  CONSTRAINT `sales_user_device_detail_ibfk_1` FOREIGN KEY (`sales_userId`) REFERENCES `sales_user` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


-- 2024-06-25 05:49:06
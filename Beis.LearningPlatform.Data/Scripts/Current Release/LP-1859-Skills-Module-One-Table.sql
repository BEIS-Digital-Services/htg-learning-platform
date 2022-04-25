-- Jira: LP-1859
-- Db: Learning Platform
-- Table: public.SkillsOneResponse


-- DROP TABLE IF EXISTS public."SkillsOneResponse";

CREATE TABLE IF NOT EXISTS public."SkillsOneResponse"
(
"Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
"UserEmailAddress" text COLLATE pg_catalog."default",
"SessionId" text COLLATE pg_catalog."default",
"Date" date,
url text COLLATE pg_catalog."default",
"InterestedInNewOpportunities" text COLLATE pg_catalog."default",
"InterestedInIncreasingSales" text COLLATE pg_catalog."default",
"InterestedInAutomatingTasks" text COLLATE pg_catalog."default",
"InterestedInRealTimeData" text COLLATE pg_catalog."default",
"BiggestFriction" text COLLATE pg_catalog."default",
"HowMuchSoftwareUsed" text COLLATE pg_catalog."default",
"ShareInfoInPerson" text COLLATE pg_catalog."default",
"ShareInfoSharedDatabase" text COLLATE pg_catalog."default",
"ShareInfoMeetings" text COLLATE pg_catalog."default",
"ShareInfoInformationConversations" text COLLATE pg_catalog."default",
"ShareInfoSomethingElse" text COLLATE pg_catalog."default",
"ShareInfoAdditionalInfo" text COLLATE pg_catalog."default",
"DigitalAdoption" text COLLATE pg_catalog."default",
CONSTRAINT "SkillsOneResponse_pkey" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."SkillsOneResponse"
OWNER to admin_dev;
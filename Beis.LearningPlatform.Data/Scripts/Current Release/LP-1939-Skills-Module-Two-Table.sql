-- Jira: LP-1939
-- Db: Learning Platform
-- Table: public.SkillsTwoResponse

-- DROP TABLE IF EXISTS public."SkillsTwoResponse";

CREATE TABLE IF NOT EXISTS public."SkillsTwoResponse"
(
    "Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "UserEmailAddress" text COLLATE pg_catalog."default",
    "SessionId" text COLLATE pg_catalog."default",
    "Date" date,
    url text COLLATE pg_catalog."default",
    "UseTechnologyForCommunication" text COLLATE pg_catalog."default",
    "ImproveCommunication" text COLLATE pg_catalog."default",
    "UseCloudStorage" text COLLATE pg_catalog."default",
    "ImproveDataStorage" text COLLATE pg_catalog."default",
    "UseOnlinePayments" text COLLATE pg_catalog."default",
    "ImprovePayments" text COLLATE pg_catalog."default",
    "UseOnlineAdvertise" text COLLATE pg_catalog."default",
    "ImproveOnlineMarketing" text COLLATE pg_catalog."default",
    "UseOnlineShop" text COLLATE pg_catalog."default",
    "ImproveOnlineBusiness" text COLLATE pg_catalog."default",
    "UsePersonaliseMessagesToCustomers" text COLLATE pg_catalog."default",
    "ImprovePersonalisedMarketing" text COLLATE pg_catalog."default",
    "UseSoftwareForPlanning" text COLLATE pg_catalog."default",
    "ImprovePlanning" text COLLATE pg_catalog."default",
    "UseDigitalTraining" text COLLATE pg_catalog."default",
    "ImproveDigitalTraining" text COLLATE pg_catalog."default",
    "UseSoftwareForInformationSharing" text COLLATE pg_catalog."default",
    "ImproveInformationSharing" text COLLATE pg_catalog."default",
    CONSTRAINT "SkillsTwoResponse_pkey" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."SkillsTwoResponse"
    OWNER to admin_dev;
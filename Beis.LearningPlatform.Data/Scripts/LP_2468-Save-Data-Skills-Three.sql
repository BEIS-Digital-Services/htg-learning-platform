-- Table: public.SkillsThreeResponse

-- DROP TABLE IF EXISTS public."SkillsThreeResponse";

CREATE TABLE IF NOT EXISTS public."SkillsThreeResponse"
(
    "Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "UserEmailAddress" text COLLATE pg_catalog."default",
    "SessionId" text COLLATE pg_catalog."default",
    "Date" date,
    url text COLLATE pg_catalog."default",
    "Questionnaire" text COLLATE pg_catalog."default",
    "WhyNeedStart" text COLLATE pg_catalog."default",
    "WhyNeedNext" text COLLATE pg_catalog."default",
    "WhyNeedFinally" text COLLATE pg_catalog."default",
    "HowAccessStart" text COLLATE pg_catalog."default",
    "HowAccessNext" text COLLATE pg_catalog."default",
    "HowAccessFinally" text COLLATE pg_catalog."default",
    "RiskStart" text COLLATE pg_catalog."default",
    "RiskNext" text COLLATE pg_catalog."default",
    "RiskFinally" text COLLATE pg_catalog."default",
    CONSTRAINT "SkillsThreeResponse_pkey" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."SkillsThreeResponse"
    OWNER to admin_dev;
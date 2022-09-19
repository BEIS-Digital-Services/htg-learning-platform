-- this is required for linking local storage to db. HTGCI-32
-- Column: public."SkillsThreeResponse"."UniqueId"

ALTER TABLE IF EXISTS public."SkillsThreeResponse"
    ADD COLUMN "UniqueId" text COLLATE pg_catalog."default";
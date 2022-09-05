

ALTER TABLE public."SkillsOneResponse" ADD COLUMN IF NOT EXISTS "IsPrivacyPolicyAccepted" BOOL NULL;
ALTER TABLE public."SkillsOneResponse" ADD COLUMN IF NOT EXISTS "IsOptedInForMarketing" BOOL NULL;

ALTER TABLE public."SkillsTwoResponse" ADD COLUMN IF NOT EXISTS "IsPrivacyPolicyAccepted" BOOL NULL;
ALTER TABLE public."SkillsTwoResponse" ADD COLUMN IF NOT EXISTS "IsOptedInForMarketing" BOOL NULL;

ALTER TABLE public."SkillsThreeResponse" ADD COLUMN IF NOT EXISTS "IsPrivacyPolicyAccepted" BOOL NULL;
ALTER TABLE public."SkillsThreeResponse" ADD COLUMN IF NOT EXISTS "IsOptedInForMarketing" BOOL NULL;

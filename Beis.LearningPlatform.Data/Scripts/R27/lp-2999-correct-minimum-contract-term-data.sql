-- help_to_grow database: 
UPDATE public.product_price
SET commitment_unit = 'month'
WHERE commitment_no = 1 AND commitment_unit = 'months';



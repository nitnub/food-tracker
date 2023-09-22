SELECT * from food f
RIGHT JOIN fodmap_main fm on f.fodmap_id = fm.id order by length(fm."name") desc;


select * from fodmap_alias;
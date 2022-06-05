DROP TABLE IF EXISTS tours CASCADE;
DROP TABLE IF EXISTS tourlogs CASCADE;

CREATE TABLE
    IF NOT EXISTS
        tours (
            t_id uuid
                constraint tours_pk
                    primary key,
            t_name varchar NOT NULL,
            t_description text,
            t_startname varchar,
            t_endname varchar,            
            t_transporttype varchar,
            t_distance decimal,
            t_estimatetime time,
            t_mappath varchar
);

CREATE TABLE
    IF NOT EXISTS
        tourlogs (
            tl_id uuid
                constraint tourlogs_pk
                    primary key,
            tl_tour uuid CONSTRAINT tourlogs_tours_t_id_fk REFERENCES tours,
            tl_date date,
            tl_time time,
            tl_difficulty integer,
            tl_comment text,
            tl_rating integer
);
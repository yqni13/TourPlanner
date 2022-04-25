DROP TABLE IF EXISTS tours CASCADE;
DROP TABLE IF EXISTS tourlogs CASCADE;

CREATE TABLE
    IF NOT EXISTS
        tours (
            t_id uuid PRIMARY KEY,
            t_name varchar NOT NULL,
            t_description text,
            t_startname varchar,
            t_endname varchar,
            t_startcoord decimal NOT NULL,
            t_endcoord decimal NOT NULL,
            t_transporttype integer NOT NULL,
            t_distance decimal NOT NULL,
            t_estimatetime time NOT NULL,
            t_mappath varchar
);

CREATE TABLE
    IF NOT EXISTS
        tourlogs (
            tl_id uuid PRIMARY KEY,
            tl_tour uuid CONSTRAINT tourlogs_tours_t_id_fk REFERENCES tours,
            tl_date date NOT NULL,
            tl_time time NOT NULL,
            tl_difficulty integer,
            tl_comment text,
            tl_rating integer
);
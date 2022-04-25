create table tp_tours
(
    tour_id serial
        constraint tp_tours_pk
            primary key,
    tour_name varchar not null
);
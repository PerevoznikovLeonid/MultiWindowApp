CREATE TYPE gender_enum AS ENUM ('Unspecified', 'Male', 'Female');

CREATE TABLE table_users (
    id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    first_name TEXT NOT NULL,
    last_name TEXT NOT NULL,
    gender gender_enum NOT NULL,
    birth_date DATE NOT NULL,
)
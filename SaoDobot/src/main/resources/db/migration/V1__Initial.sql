create extension if not exists "uuid-ossp";
CREATE TABLE public.employees
(
    id uuid not null default uuid_generate_v4() PRIMARY KEY,
    created_date timestamp without time zone NOT NULL,
    date_of_birth date,
    email character varying(255) COLLATE pg_catalog."default" NOT NULL,
    enabled boolean NOT NULL DEFAULT TRUE,
    first_name character varying(255) COLLATE pg_catalog."default" NOT NULL,
    last_updated timestamp without time zone,
    surname character varying(255) COLLATE pg_catalog."default" NOT NULL
);

CREATE TABLE public.user_group
(
    id uuid not null default uuid_generate_v4() PRIMARY KEY,
    name character varying(255) COLLATE pg_catalog."default" NOT NULL
);

CREATE TABLE public.users
(
    id uuid not null default uuid_generate_v4() PRIMARY KEY,
    created_date timestamp without time zone NOT NULL,
    email character varying(255) COLLATE pg_catalog."default" NOT NULL,
    last_login timestamp without time zone,
    last_updated timestamp without time zone,
    password character varying(255) COLLATE pg_catalog."default" NOT NULL,
    employee_id uuid NOT NULL,
    user_group_id uuid NOT NULL,
    CONSTRAINT PK_Users_Employees FOREIGN KEY (employee_id)
        REFERENCES public.employees (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE SET NULL,
    CONSTRAINT PK_Users_UserGroup FOREIGN KEY (user_group_id)
        REFERENCES public.user_group (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE SET NULL
);

CREATE TABLE public.user_role
(
    id uuid not null default uuid_generate_v4() PRIMARY KEY,
    name character varying(255) COLLATE pg_catalog."default" NOT NULL
);

CREATE TABLE public.user_group_role
(
    id uuid not null default uuid_generate_v4() PRIMARY KEY,
    user_group_id uuid,
    user_role_id uuid,
    CONSTRAINT PK_UserGroupRole_UserRole FOREIGN KEY (user_role_id)
        REFERENCES public.user_role (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE SET NULL,
    CONSTRAINT PK_UserGroupRole_UserGroup FOREIGN KEY (user_group_id)
        REFERENCES public.user_group (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE SET NULL
);
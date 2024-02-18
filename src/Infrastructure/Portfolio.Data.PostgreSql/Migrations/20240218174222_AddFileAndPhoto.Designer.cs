﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Portfolio.Data.PostgreSql;

#nullable disable

namespace Portfolio.Data.PostgreSql.Migrations
{
    [DbContext(typeof(EfContext))]
    [Migration("20240218174222_AddFileAndPhoto")]
    partial class AddFileAndPhoto
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Portfolio.Domain.Entities.CourseProject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date")
                        .HasDefaultValueSql("now() at time zone 'utc'")
                        .HasComment("Дата создания записи");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date")
                        .HasComment("Дата изменения записи");

                    b.Property<int>("PointNumber")
                        .HasColumnType("integer")
                        .HasColumnName("point_number")
                        .HasComment("Количество баллов");

                    b.Property<Guid>("PortfolioId")
                        .HasColumnType("uuid")
                        .HasColumnName("portfolio_id");

                    b.Property<int>("ScoreNumber")
                        .HasColumnType("integer")
                        .HasColumnName("score_number")
                        .HasComment("Оценка");

                    b.Property<int>("SemesterNumber")
                        .HasColumnType("integer")
                        .HasColumnName("semester_number")
                        .HasComment("Номер семестра");

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("subject_name")
                        .HasComment("Наименование дисциплины");

                    b.Property<string>("TopicName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("topic_name")
                        .HasComment("Наименование темы");

                    b.Property<DateTime>("СompletionDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("сompletion_date")
                        .HasComment("Дата сдачи");

                    b.HasKey("Id")
                        .HasName("pk_course_project");

                    b.HasIndex("PortfolioId")
                        .HasDatabaseName("ix_course_project_portfolio_id");

                    b.ToTable("course_project", "public");

                    b.HasComment("Курсовой проект");
                });

            modelBuilder.Entity("Portfolio.Domain.Entities.Faculty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date")
                        .HasDefaultValueSql("now() at time zone 'utc'")
                        .HasComment("Дата создания записи");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("full_name")
                        .HasComment("Полное имя");

                    b.Property<Guid>("InstituteId")
                        .HasColumnType("uuid")
                        .HasColumnName("institute_id")
                        .HasComment("Идентификатор института");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date")
                        .HasComment("Дата изменения записи");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("short_name")
                        .HasComment("Сокращенное имя");

                    b.HasKey("Id")
                        .HasName("pk_faculty");

                    b.HasIndex("InstituteId")
                        .HasDatabaseName("ix_faculty_institute_id");

                    b.ToTable("faculty", "public");

                    b.HasComment("Кафедра");
                });

            modelBuilder.Entity("Portfolio.Domain.Entities.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<string>("ContentType")
                        .HasColumnType("text")
                        .HasColumnName("content_type");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date")
                        .HasDefaultValueSql("now() at time zone 'utc'")
                        .HasComment("Дата создания записи");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("file_name");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date")
                        .HasComment("Дата изменения записи");

                    b.Property<long>("Size")
                        .HasColumnType("bigint")
                        .HasColumnName("size");

                    b.HasKey("Id")
                        .HasName("pk_file");

                    b.ToTable("file", "public");

                    b.HasComment("Файл");
                });

            modelBuilder.Entity("Portfolio.Domain.Entities.Institute", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date")
                        .HasDefaultValueSql("now() at time zone 'utc'")
                        .HasComment("Дата создания записи");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("full_name")
                        .HasComment("Полное имя");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date")
                        .HasComment("Дата изменения записи");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("short_name")
                        .HasComment("Сокращенное имя");

                    b.HasKey("Id")
                        .HasName("pk_institute");

                    b.ToTable("institute", "public");

                    b.HasComment("Институт");
                });

            modelBuilder.Entity("Portfolio.Domain.Entities.MyPortfolio", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birthday")
                        .HasComment("Дата рождения");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date")
                        .HasDefaultValueSql("now() at time zone 'utc'")
                        .HasComment("Дата создания записи");

                    b.Property<int?>("EducationLevel")
                        .HasColumnType("integer")
                        .HasColumnName("education_level")
                        .HasComment("Уровень образования");

                    b.Property<Guid?>("FacultyId")
                        .HasColumnType("uuid")
                        .HasColumnName("faculty_id")
                        .HasComment("Идентификатор кафедры");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name")
                        .HasComment("Имя");

                    b.Property<string>("GroupNumber")
                        .HasColumnType("text")
                        .HasColumnName("group_number")
                        .HasComment("Номер группы");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name")
                        .HasComment("Фамилия");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date")
                        .HasComment("Дата изменения записи");

                    b.Property<string>("Surname")
                        .HasColumnType("text")
                        .HasColumnName("surname")
                        .HasComment("Отчество");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id")
                        .HasComment("Идентификатор пользователя");

                    b.HasKey("Id")
                        .HasName("pk_portfolio");

                    b.HasIndex("FacultyId")
                        .HasDatabaseName("ix_portfolio_faculty_id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_portfolio_user_id");

                    b.ToTable("portfolio", "public");

                    b.HasComment("Портфолио");
                });

            modelBuilder.Entity("Portfolio.Domain.Entities.Photo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date")
                        .HasDefaultValueSql("now() at time zone 'utc'")
                        .HasComment("Дата создания записи");

                    b.Property<Guid>("FileId")
                        .HasColumnType("uuid")
                        .HasColumnName("file_id")
                        .HasComment("Идентификатор файла");

                    b.Property<bool>("IsAvatar")
                        .HasColumnType("boolean")
                        .HasColumnName("is_avatar")
                        .HasComment("Является ли фотография аватаркой");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date")
                        .HasComment("Дата изменения записи");

                    b.Property<Guid>("PortfolioId")
                        .HasColumnType("uuid")
                        .HasColumnName("portfolio_id")
                        .HasComment("Идентификатор портфолио");

                    b.HasKey("Id")
                        .HasName("pk_photo");

                    b.HasIndex("FileId")
                        .HasDatabaseName("ix_photo_file_id");

                    b.HasIndex("PortfolioId")
                        .HasDatabaseName("ix_photo_portfolio_id");

                    b.ToTable("photo", "public");

                    b.HasComment("Фотография");
                });

            modelBuilder.Entity("Portfolio.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date")
                        .HasDefaultValueSql("now() at time zone 'utc'")
                        .HasComment("Дата создания записи");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date")
                        .HasComment("Дата изменения записи");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name")
                        .HasComment("Наименование");

                    b.HasKey("Id")
                        .HasName("pk_role");

                    b.ToTable("role", "public");

                    b.HasComment("Роль");
                });

            modelBuilder.Entity("Portfolio.Domain.Entities.RolePrivilege", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date")
                        .HasDefaultValueSql("now() at time zone 'utc'")
                        .HasComment("Дата создания записи");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date")
                        .HasComment("Дата изменения записи");

                    b.Property<int>("Privilege")
                        .HasColumnType("integer")
                        .HasColumnName("privilege")
                        .HasComment("Право доступа");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id")
                        .HasComment("Идентификатор роли");

                    b.HasKey("Id")
                        .HasName("pk_role_privilege");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_role_privilege_role_id");

                    b.ToTable("role_privilege", "public");

                    b.HasComment("Право доступа для роли");
                });

            modelBuilder.Entity("Portfolio.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date")
                        .HasDefaultValueSql("now() at time zone 'utc'")
                        .HasComment("Дата создания записи");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email")
                        .HasComment("Электронная почта");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("login")
                        .HasComment("Логин");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date")
                        .HasComment("Дата изменения записи");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_hash")
                        .HasComment("Хеш пароля");

                    b.Property<string>("Phone")
                        .HasColumnType("text")
                        .HasColumnName("phone")
                        .HasComment("Телефон");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id")
                        .HasComment("Идентификатор роли");

                    b.HasKey("Id")
                        .HasName("pk_user");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_user_role_id");

                    b.ToTable("user", "public");

                    b.HasComment("Пользователь");
                });

            modelBuilder.Entity("Portfolio.Domain.Entities.CourseProject", b =>
                {
                    b.HasOne("Portfolio.Domain.Entities.MyPortfolio", "Portfolio")
                        .WithMany("CourseProjects")
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired()
                        .HasConstraintName("fk_course_project_portfolios_portfolio_id");

                    b.Navigation("Portfolio");
                });

            modelBuilder.Entity("Portfolio.Domain.Entities.Faculty", b =>
                {
                    b.HasOne("Portfolio.Domain.Entities.Institute", "Institute")
                        .WithMany("Faculties")
                        .HasForeignKey("InstituteId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired()
                        .HasConstraintName("fk_faculty_institutes_institute_id");

                    b.Navigation("Institute");
                });

            modelBuilder.Entity("Portfolio.Domain.Entities.MyPortfolio", b =>
                {
                    b.HasOne("Portfolio.Domain.Entities.Faculty", "Faculty")
                        .WithMany("Portfolios")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_portfolio_faculty_faculty_id");

                    b.HasOne("Portfolio.Domain.Entities.User", "User")
                        .WithOne()
                        .HasForeignKey("Portfolio.Domain.Entities.MyPortfolio", "UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired()
                        .HasConstraintName("fk_portfolio_users_user_id");

                    b.OwnsOne("Portfolio.Domain.ValueObjects.Speciality", "Speciality", b1 =>
                        {
                            b1.Property<Guid>("MyPortfolioId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("speciality_name")
                                .HasComment("Название");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("speciality_number")
                                .HasComment("Номер");

                            b1.HasKey("MyPortfolioId");

                            b1.ToTable("portfolio", "public");

                            b1.WithOwner()
                                .HasForeignKey("MyPortfolioId")
                                .HasConstraintName("fk_portfolio_portfolio_id");
                        });

                    b.Navigation("Faculty");

                    b.Navigation("Speciality");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Portfolio.Domain.Entities.Photo", b =>
                {
                    b.HasOne("Portfolio.Domain.Entities.File", "File")
                        .WithMany("Photos")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired()
                        .HasConstraintName("fk_photo_file_file_id");

                    b.HasOne("Portfolio.Domain.Entities.MyPortfolio", "Portfolio")
                        .WithMany("Photos")
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired()
                        .HasConstraintName("fk_photo_portfolios_portfolio_id");

                    b.Navigation("File");

                    b.Navigation("Portfolio");
                });

            modelBuilder.Entity("Portfolio.Domain.Entities.RolePrivilege", b =>
                {
                    b.HasOne("Portfolio.Domain.Entities.Role", "Role")
                        .WithMany("Privileges")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired()
                        .HasConstraintName("fk_role_privilege_roles_role_id");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Portfolio.Domain.Entities.User", b =>
                {
                    b.HasOne("Portfolio.Domain.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_role_role_id");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Portfolio.Domain.Entities.Faculty", b =>
                {
                    b.Navigation("Portfolios");
                });

            modelBuilder.Entity("Portfolio.Domain.Entities.File", b =>
                {
                    b.Navigation("Photos");
                });

            modelBuilder.Entity("Portfolio.Domain.Entities.Institute", b =>
                {
                    b.Navigation("Faculties");
                });

            modelBuilder.Entity("Portfolio.Domain.Entities.MyPortfolio", b =>
                {
                    b.Navigation("CourseProjects");

                    b.Navigation("Photos");
                });

            modelBuilder.Entity("Portfolio.Domain.Entities.Role", b =>
                {
                    b.Navigation("Privileges");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}

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
    [Migration("20240204152138_AddPortfolio")]
    partial class AddPortfolio
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

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

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name")
                        .HasComment("Имя");

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

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_portfolio_user_id");

                    b.ToTable("portfolio", "public");

                    b.HasComment("Портфолио");
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

            modelBuilder.Entity("Portfolio.Domain.Entities.MyPortfolio", b =>
                {
                    b.HasOne("Portfolio.Domain.Entities.User", "User")
                        .WithOne()
                        .HasForeignKey("Portfolio.Domain.Entities.MyPortfolio", "UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired()
                        .HasConstraintName("fk_portfolio_users_user_id");

                    b.OwnsOne("Portfolio.Domain.ValueObjects.Institute", "Institute", b1 =>
                        {
                            b1.Property<Guid>("MyPortfolioId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("FullName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("institute_full_name")
                                .HasComment("Полное имя");

                            b1.Property<string>("ShortName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("institute_short_name")
                                .HasComment("Сокращенное имя");

                            b1.HasKey("MyPortfolioId");

                            b1.ToTable("portfolio", "public");

                            b1.WithOwner()
                                .HasForeignKey("MyPortfolioId")
                                .HasConstraintName("fk_portfolio_portfolio_id");
                        });

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

                    b.Navigation("Institute");

                    b.Navigation("Speciality");

                    b.Navigation("User");
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

            modelBuilder.Entity("Portfolio.Domain.Entities.Role", b =>
                {
                    b.Navigation("Privileges");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}

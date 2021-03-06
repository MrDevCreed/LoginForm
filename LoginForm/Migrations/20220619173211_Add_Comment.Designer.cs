// <auto-generated />
using System;
using LoginForm.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LoginForm.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20220619173211_Add_Comment")]
    partial class Add_Comment
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LoginForm.Domain.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Phone")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("LoginForm.Domain.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccountId");

                    b.Property<string>("Text");

                    b.Property<string>("Titel");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("LoginForm.Domain.UserActivity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body");

                    b.Property<string>("Cookie");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("IP");

                    b.Property<string>("Url");

                    b.Property<string>("UserAgent");

                    b.HasKey("Id");

                    b.ToTable("UserActivities");
                });

            modelBuilder.Entity("LoginForm.Domain.Comment", b =>
                {
                    b.HasOne("LoginForm.Domain.Account")
                        .WithMany("Comments")
                        .HasForeignKey("AccountId");
                });
#pragma warning restore 612, 618
        }
    }
}

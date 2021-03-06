// <auto-generated />
using System;
using BancoTDD.Infraestrutura;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BancoTDD.Infraestrutura.Migrations
{
    [DbContext(typeof(BancoContext))]
    partial class BancoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BancoTDD.Dominio.CuentasBancarias.CuentaBancaria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Numero")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Saldo")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("CuentaBancaria");

                    b.HasDiscriminator<string>("Discriminator").HasValue("CuentaBancaria");
                });

            modelBuilder.Entity("BancoTDD.Dominio.CuentasBancarias.Movimiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CuentaBancariaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Tipo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CuentaBancariaId");

                    b.ToTable("Movimiento");
                });

            modelBuilder.Entity("BancoTDD.Dominio.CuentaAhorro", b =>
                {
                    b.HasBaseType("BancoTDD.Dominio.CuentasBancarias.CuentaBancaria");

                    b.Property<string>("Ciudad")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("CuentaAhorro");
                });

            modelBuilder.Entity("BancoTDD.Dominio.CuentasBancarias.Movimiento", b =>
                {
                    b.HasOne("BancoTDD.Dominio.CuentasBancarias.CuentaBancaria", "CuentaBancaria")
                        .WithMany("Movimientos")
                        .HasForeignKey("CuentaBancariaId");

                    b.Navigation("CuentaBancaria");
                });

            modelBuilder.Entity("BancoTDD.Dominio.CuentasBancarias.CuentaBancaria", b =>
                {
                    b.Navigation("Movimientos");
                });
#pragma warning restore 612, 618
        }
    }
}

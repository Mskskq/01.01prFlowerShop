using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Flower.Models;

public partial class FlowersContext : DbContext
{
    public FlowersContext()
    {
    }

    public FlowersContext(DbContextOptions<FlowersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adress> Adresses { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Corsina> Corsinas { get; set; }

    public virtual DbSet<Flower> Flowers { get; set; }

    public virtual DbSet<FlowersInOrder> FlowersInOrders { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Streat> Streats { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Flowers;Username=postgres;Password=admin");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Adress_pkey");

            entity.ToTable("Adress");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdStreat).HasColumnName("ID_Streat");
            entity.Property(e => e.NumberHouse)
                .HasMaxLength(20)
                .HasColumnName("Number_House");
            entity.Property(e => e.NumberOpartamens).HasColumnName("Number_Opartamens");

            entity.HasOne(d => d.IdStreatNavigation).WithMany(p => p.Adresses)
                .HasForeignKey(d => d.IdStreat)
                .HasConstraintName("Adress_ID_Streat_fkey");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("City_pkey");

            entity.ToTable("City");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdRegion).HasColumnName("ID_Region");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.IdRegionNavigation).WithMany(p => p.Cities)
                .HasForeignKey(d => d.IdRegion)
                .HasConstraintName("City_ID_Region_fkey");
        });

        modelBuilder.Entity<Corsina>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Corsina_pkey");

            entity.ToTable("Corsina");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FlowersId).HasColumnName("Flowers_ID");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.Flowers).WithMany(p => p.Corsinas)
                .HasForeignKey(d => d.FlowersId)
                .HasConstraintName("Corsina_Flowers_ID_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Corsinas)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Corsina_User_ID_fkey");
        });

        modelBuilder.Entity<Flower>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Flowers_pkey");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Img)
                .HasMaxLength(100)
                .HasColumnName("img");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
        });

        modelBuilder.Entity<FlowersInOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Flowers_In_Order_pkey");

            entity.ToTable("Flowers_In_Order");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FlowersId).HasColumnName("Flowers_ID");
            entity.Property(e => e.OrdeId).HasColumnName("Orde_ID");

            entity.HasOne(d => d.Flowers).WithMany(p => p.FlowersInOrders)
                .HasForeignKey(d => d.FlowersId)
                .HasConstraintName("Flowers_In_Order_Flowers_ID_fkey");

            entity.HasOne(d => d.Orde).WithMany(p => p.FlowersInOrders)
                .HasForeignKey(d => d.OrdeId)
                .HasConstraintName("Flowers_In_Order_Orde_ID_fkey");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Orders_pkey");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cost)
                .HasPrecision(10, 2)
                .HasColumnName("cost");
            entity.Property(e => e.DeliveryTime).HasColumnName("Delivery_time");
            entity.Property(e => e.IdAdress).HasColumnName("ID_Adress");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.IdAdressNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdAdress)
                .HasConstraintName("Orders_ID_Adress_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Orders_User_ID_fkey");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Region_pkey");

            entity.ToTable("Region");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Streat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Streat_pkey");

            entity.ToTable("Streat");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdCity).HasColumnName("ID_City");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.IdCityNavigation).WithMany(p => p.Streats)
                .HasForeignKey(d => d.IdCity)
                .HasConstraintName("Streat_ID_City_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Users_pkey");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Lastname).HasMaxLength(100);
            entity.Property(e => e.Login).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Surname).HasMaxLength(100);
            entity.Property(e => e.Telephone).HasMaxLength(100);

            entity.HasOne(d => d.AddressNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Address)
                .HasConstraintName("Users_address_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TrackingManagement.Models
{
    public partial class BKContext : DbContext
    {
        public BKContext()
        {
        }

        public BKContext(DbContextOptions<BKContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Atmtechnican> Atmtechnicans { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<DailyCar> DailyCars { get; set; }
        public virtual DbSet<DailyKmCar> DailyKmCars { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<EditedSegmentationRoute> EditedSegmentationRoutes { get; set; }
        public virtual DbSet<History> Histories { get; set; }
        public virtual DbSet<Online> Onlines { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Rfid> Rfids { get; set; }
        public virtual DbSet<SampleRoute> SampleRoutes { get; set; }
        public virtual DbSet<Scope> Scopes { get; set; }
        public virtual DbSet<ScopePermission> ScopePermissions { get; set; }
        public virtual DbSet<Segmentation> Segmentations { get; set; }
        public virtual DbSet<TransactionPoint> TransactionPoints { get; set; }
        public virtual DbSet<Treasurer> Treasurers { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserUnit> UserUnits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=BK;Trusted_Connection=False;User Id=sa;Password=Boladinh99.;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Atmtechnican>(entity =>
            {
                entity.ToTable("ATMTechnicans");

                entity.Property(e => e.Avatar).HasMaxLength(255);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeCode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("NAME");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Rfidid).HasColumnName("RFIDId");

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Rfid)
                    .WithMany(p => p.Atmtechnicans)
                    .HasForeignKey(d => d.Rfidid)
                    .HasConstraintName("FK__ATMTechni__RFIDI__44FF419A");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Atmtechnicans)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK__ATMTechni__UnitI__440B1D61");
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.Property(e => e.DriverId).HasColumnName("Driver_Id");

                entity.Property(e => e.FirstCamPo).HasDefaultValueSql("((0))");

                entity.Property(e => e.Fuel).HasColumnName("fuel");

                entity.Property(e => e.LicensePlate)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.RfidId).HasColumnName("RFID_Id");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UnitId).HasColumnName("Unit_Id");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.DriverId)
                    .HasConstraintName("FK__Cars__Driver_Id__52593CB8");

                entity.HasOne(d => d.Rfid)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.RfidId)
                    .HasConstraintName("FK__Cars__RFID_Id__5165187F");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK__Cars__Unit_Id__5070F446");
            });

            modelBuilder.Entity<DailyCar>(entity =>
            {
                entity.ToTable("DailyCar");

                entity.Property(e => e.CarLicensePlate)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReportTime).HasColumnType("datetime");

                entity.Property(e => e.RouteCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.DailyCars)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK__DailyCar__CarId__73BA3083");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.DailyCars)
                    .HasForeignKey(d => d.RouteId)
                    .HasConstraintName("FK__DailyCar__RouteI__74AE54BC");
            });

            modelBuilder.Entity<DailyKmCar>(entity =>
            {
                entity.ToTable("DailyKmCar");

                entity.Property(e => e.CarLicensePlate)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Driver).HasMaxLength(255);

                entity.Property(e => e.ReportTime).HasColumnType("datetime");

                entity.Property(e => e.Treasure).HasMaxLength(255);

                entity.Property(e => e.UnitName).HasMaxLength(20);

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.DailyKmCars)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK__DailyKmCa__CarId__787EE5A0");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.DailyKmCars)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK__DailyKmCa__UnitI__778AC167");
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.Property(e => e.ActivationTime).HasColumnType("date");

                entity.Property(e => e.AllowUpdate).HasDefaultValueSql("((1))");

                entity.Property(e => e.CarId).HasColumnName("Car_Id");

                entity.Property(e => e.DeviceNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Imei)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IMEI");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.UnitId).HasColumnName("Unit_Id");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK__Devices__Car_Id__59063A47");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK__Devices__Unit_Id__5812160E");
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.Property(e => e.Avatar).HasMaxLength(255);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeCode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("NAME");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Rfidid).HasColumnName("RFIDId");

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Rfid)
                    .WithMany(p => p.Drivers)
                    .HasForeignKey(d => d.Rfidid)
                    .HasConstraintName("FK__Drivers__RFIDId__403A8C7D");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Drivers)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK__Drivers__UnitId__3F466844");
            });

            modelBuilder.Entity<EditedSegmentationRoute>(entity =>
            {
                entity.Property(e => e.Direction).IsRequired();

                entity.Property(e => e.EditedIn).HasColumnType("date");

                entity.Property(e => e.Route)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.WayBack).IsRequired();

                entity.HasOne(d => d.Segmentation)
                    .WithMany(p => p.EditedSegmentationRoutes)
                    .HasForeignKey(d => d.SegmentationId)
                    .HasConstraintName("FK__EditedSeg__Segme__70DDC3D8");
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.HasIndex(e => e.DeviceTime, "idx_deviceTime");

                entity.Property(e => e.AppVersion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cam1ImgPath).HasMaxLength(256);

                entity.Property(e => e.Cam2ImgPath).HasMaxLength(256);

                entity.Property(e => e.DeviceTime).HasColumnType("datetime");

                entity.Property(e => e.ReceivedTime).HasColumnType("datetime");

                entity.Property(e => e.Rfidstring)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("RFIDString");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Histories)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK__Histories__CarId__6D0D32F4");
            });

            modelBuilder.Entity<Online>(entity =>
            {
                entity.Property(e => e.AppVersion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cam1ImgPath).HasMaxLength(256);

                entity.Property(e => e.Cam2ImgPath).HasMaxLength(256);

                entity.Property(e => e.DeviceTime).HasColumnType("datetime");

                entity.Property(e => e.ReceivedTime).HasColumnType("datetime");

                entity.Property(e => e.Rfidstring)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("RFIDString");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Onlines)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK__Onlines__CarId__6A30C649");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Filter)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FilterValue)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Method)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Resource)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Rfid>(entity =>
            {
                entity.ToTable("RFIDs");

                entity.Property(e => e.ActivationTime).HasColumnType("date");

                entity.Property(e => e.CardNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Rfids)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK__RFIDs__UnitId__3B75D760");
            });

            modelBuilder.Entity<SampleRoute>(entity =>
            {
                entity.Property(e => e.Direction).IsRequired();

                entity.Property(e => e.Permanent)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Route)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.RouteCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UnitId).HasColumnName("Unit_Id");

                entity.Property(e => e.WayBack).IsRequired();

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.SampleRoutes)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK__SampleRou__Unit___5DCAEF64");
            });

            modelBuilder.Entity<Scope>(entity =>
            {
                entity.Property(e => e.AllowedRoute)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<ScopePermission>(entity =>
            {
                entity.ToTable("Scope_Permission");

                entity.Property(e => e.Allowed)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Filter)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PermissionId).HasColumnName("Permission_Id");

                entity.Property(e => e.ScopeId).HasColumnName("Scope_Id");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.ScopePermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .HasConstraintName("FK__Scope_Per__Permi__2B3F6F97");

                entity.HasOne(d => d.Scope)
                    .WithMany(p => p.ScopePermissions)
                    .HasForeignKey(d => d.ScopeId)
                    .HasConstraintName("FK__Scope_Per__Scope__2C3393D0");
            });

            modelBuilder.Entity<Segmentation>(entity =>
            {
                entity.Property(e => e.AtmtechnicanId).HasColumnName("ATMTechnican_Id");

                entity.Property(e => e.BeginDate).HasColumnType("date");

                entity.Property(e => e.CarId).HasColumnName("Car_Id");

                entity.Property(e => e.Control)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Day)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DriverId).HasColumnName("Driver_Id");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.RouteId).HasColumnName("Route_Id");

                entity.Property(e => e.Sms)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TreasurerId).HasColumnName("Treasurer_Id");

                entity.Property(e => e.UnitId).HasColumnName("Unit_Id");

                entity.HasOne(d => d.Atmtechnican)
                    .WithMany(p => p.Segmentations)
                    .HasForeignKey(d => d.AtmtechnicanId)
                    .HasConstraintName("FK__Segmentat__ATMTe__6754599E");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Segmentations)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK__Segmentat__Car_I__6383C8BA");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Segmentations)
                    .HasForeignKey(d => d.DriverId)
                    .HasConstraintName("FK__Segmentat__Drive__6477ECF3");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.Segmentations)
                    .HasForeignKey(d => d.RouteId)
                    .HasConstraintName("FK__Segmentat__Route__656C112C");

                entity.HasOne(d => d.Treasurer)
                    .WithMany(p => p.Segmentations)
                    .HasForeignKey(d => d.TreasurerId)
                    .HasConstraintName("FK__Segmentat__Treas__66603565");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Segmentations)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK__Segmentat__Unit___628FA481");
            });

            modelBuilder.Entity<TransactionPoint>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Branch).HasMaxLength(255);

                entity.Property(e => e.Contact).HasMaxLength(50);

                entity.Property(e => e.Fax).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.PointCode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PointName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.TransactionPoints)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK__Transacti__UnitI__4CA06362");
            });

            modelBuilder.Entity<Treasurer>(entity =>
            {
                entity.ToTable("Treasurer");

                entity.Property(e => e.Avatar).HasMaxLength(255);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeCode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("NAME");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Rfidid).HasColumnName("RFIDId");

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Rfid)
                    .WithMany(p => p.Treasurers)
                    .HasForeignKey(d => d.Rfidid)
                    .HasConstraintName("FK__Treasurer__RFIDI__49C3F6B7");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Treasurers)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK__Treasurer__UnitI__48CFD27E");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ScopeId).HasColumnName("Scope_Id");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Scope)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ScopeId)
                    .HasConstraintName("FK__Users__Scope_Id__300424B4");
            });

            modelBuilder.Entity<UserUnit>(entity =>
            {
                entity.ToTable("User_Unit");

                entity.Property(e => e.UnitId).HasColumnName("Unit_Id");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.UserUnits)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User_Unit__Unit___35BCFE0A");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserUnits)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User_Unit__User___34C8D9D1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

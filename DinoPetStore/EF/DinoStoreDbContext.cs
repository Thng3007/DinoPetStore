using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DinoPetStore.EF
{
    public partial class DinoStoreDbContext : DbContext
    {
        public DinoStoreDbContext()
            : base("name=DinoStoreDbContext")
        {
        }

        public virtual DbSet<ADMIN> ADMINs { get; set; }
        public virtual DbSet<CHUCNANG_QUYEN> CHUCNANG_QUYENs { get; set; }
        public virtual DbSet<CTDONDATHANG> CTDONDATHANGs { get; set; }
        public virtual DbSet<DONDATHANG> DONDATHANGs { get; set; }
        public virtual DbSet<GIAMGIA> GIAMGIAs { get; set; }
        public virtual DbSet<HINH> HINHs { get; set; }
        public virtual DbSet<KHACHHANG> KHACHHANGs { get; set; }
        public virtual DbSet<KICHTHUOC> KICHTHUOCs { get; set; }
        public virtual DbSet<LOAI> LOAIs { get; set; }
        public virtual DbSet<MAUSAC> MAUSACs { get; set; }
        public virtual DbSet<PHANQUYEN> PHANQUYENs { get; set; }
        public virtual DbSet<PHIEUNHAPKHO> PHIEUNHAPKHOes { get; set; }
        public virtual DbSet<SANPHAM> SANPHAMs { get; set; }
        public virtual DbSet<THUONGHIEU> THUONGHIEUx { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ADMIN>()
                .Property(e => e.MATKHAU)
                .IsUnicode(false);

            modelBuilder.Entity<ADMIN>()
                .HasMany(e => e.PHANQUYENs)
                .WithRequired(e => e.ADMIN)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CHUCNANG_QUYEN>()
                .Property(e => e.MACHUCNANG)
                .IsUnicode(false);

            modelBuilder.Entity<CHUCNANG_QUYEN>()
                .HasMany(e => e.PHANQUYENs)
                .WithRequired(e => e.CHUCNANG_QUYEN)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CTDONDATHANG>()
                .Property(e => e.THANHTIEN)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DONDATHANG>()
                .Property(e => e.TONGTIEN)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DONDATHANG>()
                .HasMany(e => e.CTDONDATHANGs)
                .WithRequired(e => e.DONDATHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KHACHHANG>()
                .Property(e => e.MATKHAUKH)
                .IsUnicode(false);

            modelBuilder.Entity<KHACHHANG>()
                .Property(e => e.KHOIPHUCMATKHAU)
                .IsFixedLength();

            modelBuilder.Entity<KHACHHANG>()
                .HasMany(e => e.DONDATHANGs)
                .WithRequired(e => e.KHACHHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LOAI>()
                .HasMany(e => e.SANPHAMs)
                .WithRequired(e => e.LOAI)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PHANQUYEN>()
                .Property(e => e.MACHUCNANG)
                .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.DONGIAMUA)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.DONGIABAN)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SANPHAM>()
                .HasMany(e => e.CTDONDATHANGs)
                .WithRequired(e => e.SANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SANPHAM>()
                .HasMany(e => e.GIAMGIAs)
                .WithRequired(e => e.SANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SANPHAM>()
                .HasMany(e => e.HINHs)
                .WithRequired(e => e.SANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SANPHAM>()
                .HasMany(e => e.KICHTHUOCs)
                .WithRequired(e => e.SANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SANPHAM>()
                .HasMany(e => e.PHIEUNHAPKHOes)
                .WithRequired(e => e.SANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<THUONGHIEU>()
                .HasMany(e => e.SANPHAMs)
                .WithRequired(e => e.THUONGHIEU)
                .WillCascadeOnDelete(false);
        }
    }
}

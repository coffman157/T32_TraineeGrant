using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace T32_TraineeGrant;

public partial class BumcOrgContext : DbContext
{
    public BumcOrgContext()
    {
    }

    public BumcOrgContext(DbContextOptions<BumcOrgContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<TraineeGrant> TraineeGrants { get; set; }

    public virtual DbSet<TrainingGrant> TrainingGrants { get; set; }

    public virtual DbSet<TrainingRecord> TrainingRecords { get; set; }

    public virtual DbSet<TrainingRecordAbstract> TrainingRecordAbstracts { get; set; }

    public virtual DbSet<TrainingRecordManuscript> TrainingRecordManuscripts { get; set; }

    public virtual DbSet<TrainingRecordPatent> TrainingRecordPatents { get; set; }

    public virtual DbSet<TrainingRecordVideo> TrainingRecordVideos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=sql.bumc.bu.edu;Database=BUMC_ORG; TrustServerCertificate=true;Trusted_Connection=True;MultipleActiveResultSets=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_T32_Questionnaire.person");

            entity.ToTable("person", "T32_Questionnaire");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Acceptedpredoc)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("acceptedpredoc");
            entity.Property(e => e.Buid)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("buid");
            entity.Property(e => e.Citizenship)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("citizenship");
            entity.Property(e => e.Disability)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("disability");
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Facultymentor)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("facultymentor");
            entity.Property(e => e.Firstname)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("lastname");
            entity.Property(e => e.Leaveofabsence)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("leaveofabsence");
            entity.Property(e => e.Minority)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("minority");
            entity.Property(e => e.Orcidid)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("orcidid");
            entity.Property(e => e.Position)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("position");
            entity.Property(e => e.Positionother)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("positionother");
            entity.Property(e => e.Woman)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("woman");
            entity.Property(e => e.Yearsposition)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("yearsposition");
        });

        modelBuilder.Entity<TraineeGrant>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Trainee_grants", "T32_Questionnaire");

            entity.Property(e => e.Buid)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("buid");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Personid).HasColumnName("personid");
            entity.Property(e => e.Presentations)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("presentations");
            entity.Property(e => e.Researchfaculty)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("researchfaculty");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.Statusother)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("statusother");
            entity.Property(e => e.Stilltrainee)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("stilltrainee");
            entity.Property(e => e.Title)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.Trainingendmm).HasColumnName("trainingendmm");
            entity.Property(e => e.Trainingendyy).HasColumnName("trainingendyy");
            entity.Property(e => e.Trainingrantid).HasColumnName("trainingrantid");
            entity.Property(e => e.Trainingstartmm).HasColumnName("trainingstartmm");
            entity.Property(e => e.Trainingstartyy).HasColumnName("trainingstartyy");
        });

        modelBuilder.Entity<TrainingGrant>(entity =>
        {
            entity.ToTable("TrainingGrant", "T32_Questionnaire");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        modelBuilder.Entity<TrainingRecord>(entity =>
        {
            entity.ToTable("TrainingRecord", "T32_Questionnaire");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Buid)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("buid");
            entity.Property(e => e.Personid).HasColumnName("personid");
            entity.Property(e => e.Presentations)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("presentations");
            entity.Property(e => e.Researchfaculty)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("researchfaculty");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.Statusother)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("statusother");
            entity.Property(e => e.Stilltrainee)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("stilltrainee");
            entity.Property(e => e.Trainingendmm).HasColumnName("trainingendmm");
            entity.Property(e => e.Trainingendyy).HasColumnName("trainingendyy");
            entity.Property(e => e.Trainingrantid).HasColumnName("trainingrantid");
            entity.Property(e => e.Trainingstartmm).HasColumnName("trainingstartmm");
            entity.Property(e => e.Trainingstartyy).HasColumnName("trainingstartyy");
        });

        modelBuilder.Entity<TrainingRecordAbstract>(entity =>
        {
            entity.ToTable("TrainingRecordAbstract", "T32_Questionnaire");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Authors)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("authors");
            entity.Property(e => e.Buid)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("buid");
            entity.Property(e => e.City)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Conference)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("conference");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.PosterOral)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("poster_oral");
            entity.Property(e => e.Presenter)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("presenter");
            entity.Property(e => e.Title)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.Trainingrecordid).HasColumnName("trainingrecordid");
            entity.Property(e => e.Type)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("type");
        });

        modelBuilder.Entity<TrainingRecordManuscript>(entity =>
        {
            entity.ToTable("TrainingRecordManuscript", "T32_Questionnaire");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Authors)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("authors");
            entity.Property(e => e.Buid)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("buid");
            entity.Property(e => e.DoiPmidPmcid)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("doi_pmid_pmcid");
            entity.Property(e => e.Journal)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("journal");
            entity.Property(e => e.Pages)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("pages");
            entity.Property(e => e.Status)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.Statusother)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("statusother");
            entity.Property(e => e.Title)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.Trainingrecordid).HasColumnName("trainingrecordid");
            entity.Property(e => e.Volume)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("volume");
            entity.Property(e => e.Year)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("year");
        });

        modelBuilder.Entity<TrainingRecordPatent>(entity =>
        {
            entity.ToTable("TrainingRecordPatents", "T32_Questionnaire");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Buid)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("buid");
            entity.Property(e => e.Dateissued).HasColumnName("dateissued");
            entity.Property(e => e.Inventors)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("inventors");
            entity.Property(e => e.Personid).HasColumnName("personid");
            entity.Property(e => e.Title)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.Trainingrecordid).HasColumnName("trainingrecordid");
        });

        modelBuilder.Entity<TrainingRecordVideo>(entity =>
        {
            entity.ToTable("TrainingRecordVideo", "T32_Questionnaire");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Authors)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("authors");
            entity.Property(e => e.Buid)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("buid");
            entity.Property(e => e.Dateuploaded)
                .HasColumnType("datetime")
                .HasColumnName("dateuploaded");
            entity.Property(e => e.Personid).HasColumnName("personid");
            entity.Property(e => e.Title)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.Trainingrecordid).HasColumnName("trainingrecordid");
            entity.Property(e => e.Url)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("url");
            entity.Property(e => e.Website)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("website");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

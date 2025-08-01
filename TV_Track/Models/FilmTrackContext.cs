using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TV_Track.Models
{
    public partial class FilmTrackContext : DbContext
    {
        public FilmTrackContext()
        {
        }

        public FilmTrackContext(DbContextOptions<FilmTrackContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<Cast> Casts { get; set; }
        public virtual DbSet<Director> Directors { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<List> Lists { get; set; }
        public virtual DbSet<ListContent> ListContents { get; set; }
        public virtual DbSet<ListType> ListTypes { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<NotifyOnContentRelease> NotifyOnContentReleases { get; set; }
        public virtual DbSet<TvGenre> TvGenres { get; set; }
        public virtual DbSet<TvSeries> TvSeries { get; set; }
        public virtual DbSet<TvSeriesEpisode> TvSeriesEpisodes { get; set; }
        public virtual DbSet<TvSeriesSeason> TvSeriesSeasons { get; set; }
        public virtual DbSet<WatchedMovie> WatchedMovies { get; set; }
        public virtual DbSet<WatchedTvshow> WatchedTvshows { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-FMEPUCF;Initial Catalog=FilmTrack;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.Property(e => e.ActorId).HasColumnName("ActorID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Cast>(entity =>
            {
                entity.ToTable("Cast");

                entity.Property(e => e.CastId).HasColumnName("CastID");

                entity.Property(e => e.ActorId).HasColumnName("ActorID");

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.TvSeriesId).HasColumnName("TvSeriesID");

                entity.HasOne(d => d.Actor)
                    .WithMany(p => p.Casts)
                    .HasForeignKey(d => d.ActorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cast__ActorID__5441852A");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Casts)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK__Cast__MovieID__398D8EEE");

                entity.HasOne(d => d.TvSeries)
                    .WithMany(p => p.Casts)
                    .HasForeignKey(d => d.TvSeriesId)
                    .HasConstraintName("FK__Cast__TvSeriesID__3A81B327");
            });

            modelBuilder.Entity<Director>(entity =>
            {
                entity.Property(e => e.DirectorId).HasColumnName("DirectorID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.GenreId)
                    .ValueGeneratedNever()
                    .HasColumnName("GenreID");

                entity.Property(e => e.GenreName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MediaType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<List>(entity =>
            {
                entity.Property(e => e.ListId).HasColumnName("ListID");

                entity.Property(e => e.ListName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ListTypeId).HasColumnName("ListTypeID");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.ListType)
                    .WithMany(p => p.Lists)
                    .HasForeignKey(d => d.ListTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Lists__ListTypeI__59FA5E80");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Lists)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Lists__UserID__5AEE82B9");
            });

            modelBuilder.Entity<ListContent>(entity =>
            {
                entity.ToTable("ListContent");

                entity.Property(e => e.ListContentId).HasColumnName("ListContentID");

                entity.Property(e => e.ListId).HasColumnName("ListID");

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.TvSeriesId).HasColumnName("TvSeriesID");

                entity.HasOne(d => d.List)
                    .WithMany(p => p.ListContents)
                    .HasForeignKey(d => d.ListId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ListConte__ListI__571DF1D5");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.ListContents)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK__ListConte__Movie__08B54D69");

                entity.HasOne(d => d.TvSeries)
                    .WithMany(p => p.ListContents)
                    .HasForeignKey(d => d.TvSeriesId)
                    .HasConstraintName("FK__ListConte__TvSer__09A971A2");
            });

            modelBuilder.Entity<ListType>(entity =>
            {
                entity.Property(e => e.ListTypeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.MovieId)
                    .ValueGeneratedNever()
                    .HasColumnName("MovieID");

                entity.Property(e => e.CountryName).HasMaxLength(50);

                entity.Property(e => e.DirectorId).HasColumnName("DirectorID");

                entity.Property(e => e.MoviePoster).HasMaxLength(200);

                entity.Property(e => e.ReleaseYear).HasColumnType("date");

                entity.Property(e => e.RunningTime).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.VoteAverage).HasMaxLength(50);

                entity.HasOne(d => d.Director)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.DirectorId)
                    .HasConstraintName("FK__Movies__Director__2C3393D0");
            });

            modelBuilder.Entity<NotifyOnContentRelease>(entity =>
            {
                entity.HasKey(e => e.NotifyId)
                    .HasName("PK__NotifyOn__AD54A2DC09B2BFC4");

                entity.ToTable("NotifyOnContentRelease");

                entity.Property(e => e.NotifyId).HasColumnName("NotifyID");

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.TvSeriesId).HasColumnName("TvSeriesID");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.NotifyOnContentReleases)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK__NotifyOnC__Movie__32AB8735");

                entity.HasOne(d => d.TvSeries)
                    .WithMany(p => p.NotifyOnContentReleases)
                    .HasForeignKey(d => d.TvSeriesId)
                    .HasConstraintName("FK__NotifyOnC__TvSer__339FAB6E");
            });

            modelBuilder.Entity<TvGenre>(entity =>
            {
                entity.Property(e => e.TvGenreId).HasColumnName("TvGenreID");

                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.TvSeriesId).HasColumnName("TvSeriesID");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.TvGenres)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TvGenres__GenreI__403A8C7D");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.TvGenres)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK__TvGenres__MovieI__3E52440B");

                entity.HasOne(d => d.TvSeries)
                    .WithMany(p => p.TvGenres)
                    .HasForeignKey(d => d.TvSeriesId)
                    .HasConstraintName("FK__TvGenres__TvSeri__3F466844");
            });

            modelBuilder.Entity<TvSeries>(entity =>
            {
                entity.Property(e => e.TvSeriesId)
                    .ValueGeneratedNever()
                    .HasColumnName("TvSeriesID");

                entity.Property(e => e.CountryName).HasMaxLength(50);

                entity.Property(e => e.Creator)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.EndYear).HasColumnType("date");

                entity.Property(e => e.RunningTime).HasMaxLength(50);

                entity.Property(e => e.StartYear).HasColumnType("date");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TvSeriesPoster).HasMaxLength(50);

                entity.Property(e => e.VoteAverage)
                    .HasMaxLength(50)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TvSeriesEpisode>(entity =>
            {
                entity.HasKey(e => e.TvEpisodeId)
                    .HasName("PK__TvSeries__9EDA50CCDB598247");

                entity.ToTable("TvSeries_Episodes");

                entity.Property(e => e.TvEpisodeId).HasColumnName("TvEpisodeID");

                entity.Property(e => e.AirDate).HasColumnType("date");

                entity.Property(e => e.DirectorId).HasColumnName("DirectorID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TvSeasonId).HasColumnName("TvSeasonID");

                entity.HasOne(d => d.Director)
                    .WithMany(p => p.TvSeriesEpisodes)
                    .HasForeignKey(d => d.DirectorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TvSeries___Direc__619B8048");

                entity.HasOne(d => d.TvSeason)
                    .WithMany(p => p.TvSeriesEpisodes)
                    .HasForeignKey(d => d.TvSeasonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TvSeries___TvSea__628FA481");
            });

            modelBuilder.Entity<TvSeriesSeason>(entity =>
            {
                entity.HasKey(e => e.TvSeasonId)
                    .HasName("PK__TvSeries__D434A763E45CC255");

                entity.ToTable("TvSeries_Seasons");

                entity.Property(e => e.TvSeasonId).HasColumnName("TvSeasonID");

                entity.Property(e => e.Summary).IsRequired();

                entity.Property(e => e.TvSeriesId).HasColumnName("TvSeriesID");

                entity.HasOne(d => d.TvSeries)
                    .WithMany(p => p.TvSeriesSeasons)
                    .HasForeignKey(d => d.TvSeriesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TvSeries___TvSer__32E0915F");
            });

            modelBuilder.Entity<WatchedMovie>(entity =>
            {
                entity.Property(e => e.WatchedMovieId).HasColumnName("WatchedMovieID");

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.WatchedMovies)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WatchedMo__Movie__2A164134");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.WatchedMovies)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WatchedMo__UserI__656C112C");
            });

            modelBuilder.Entity<WatchedTvshow>(entity =>
            {
                entity.HasKey(e => e.WatchedTvshowsId)
                    .HasName("PK__WatchedT__6FFBA13093E8B5B9");

                entity.ToTable("WatchedTVShows");

                entity.Property(e => e.WatchedTvshowsId).HasColumnName("WatchedTVShowsID");

                entity.Property(e => e.TvepisodeId).HasColumnName("TVEpisodeID");

                entity.Property(e => e.TvseasonId).HasColumnName("TVSeasonID");

                entity.Property(e => e.TvseriesId).HasColumnName("TVSeriesID");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Tvepisode)
                    .WithMany(p => p.WatchedTvshows)
                    .HasForeignKey(d => d.TvepisodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WatchedTV__TVEpi__66603565");

                entity.HasOne(d => d.Tvseason)
                    .WithMany(p => p.WatchedTvshows)
                    .HasForeignKey(d => d.TvseasonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WatchedTV__TVSea__6754599E");

                entity.HasOne(d => d.Tvseries)
                    .WithMany(p => p.WatchedTvshows)
                    .HasForeignKey(d => d.TvseriesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WatchedTV__TVSer__2DE6D218");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.WatchedTvshows)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WatchedTV__UserI__693CA210");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
using VetApp.ViewModels;

namespace VetApp.Models;

public partial class VeterinariaExtendidaContext : DbContext
{
    public VeterinariaExtendidaContext()
    {
    }

    public VeterinariaExtendidaContext(DbContextOptions<VeterinariaExtendidaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alimento> Alimentos { get; set; }

    public virtual DbSet<AplicaVacuna> AplicaVacunas { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Comodidade> Comodidades { get; set; }

    public virtual DbSet<Consulta> Consultas { get; set; }

    public virtual DbSet<ConsumoHotel> ConsumoHotels { get; set; }

    public virtual DbSet<ConsumosVet> ConsumosVets { get; set; }

    public virtual DbSet<HistPeso> HistPesos { get; set; }

    public virtual DbSet<Hospedaje> Hospedajes { get; set; }

    public virtual DbSet<Mascota> Mascotas { get; set; }

    public virtual DbSet<Medicamento> Medicamentos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<PersonaCliente> PersonaClientes { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Vacuna> Vacunas { get; set; }

 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alimento>(entity =>
        {
            entity.HasKey(e => e.CodAlimento).HasName("PK_Alim");

            entity.Property(e => e.CodAlimento).HasColumnName("codAlimento");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("money")
                .HasColumnName("precioUnitario");
            entity.Property(e => e.Proveedor)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("proveedor");
        });


        modelBuilder.Entity<AplicaVacuna>(entity =>
        {
            entity.HasKey(e => new { e.CodMascota, e.CodVacuna, e.FechaPrevista }).HasName("PK_AplicaVac");

            entity.ToTable("AplicaVacuna");

            entity.Property(e => e.CodMascota)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codMascota");
            entity.Property(e => e.CodVacuna)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codVacuna");
            entity.Property(e => e.FechaPrevista).HasColumnName("fechaPrevista");
            entity.Property(e => e.DosisAplicada).HasColumnName("dosisAplicada");
            entity.Property(e => e.FechaAplicacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("fechaAplicacion");

            entity.HasOne(d => d.CodMascotaNavigation).WithMany(p => p.AplicaVacunas)
                .HasForeignKey(d => d.CodMascota)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MasAV");

            entity.HasOne(d => d.CodVacunaNavigation).WithMany(p => p.AplicaVacunas)
                .HasForeignKey(d => d.CodVacuna)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VacAV");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.CodCliente);

            entity.Property(e => e.CodCliente)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codCliente");
            entity.Property(e => e.Apellido)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Banco)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("banco");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.CuentaBanco)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("cuentaBanco");
            entity.Property(e => e.Direccion)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Comodidade>(entity =>
        {
            entity.HasKey(e => e.IdComodidad).HasName("PK_Com");

            entity.Property(e => e.IdComodidad).HasColumnName("idComodidad");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("money")
                .HasColumnName("precioUnitario");
        });


        modelBuilder.Entity<Consulta>(entity =>
        {
            entity.HasKey(e => new { e.CodMascota, e.FechaConsulta });

            entity.Property(e => e.CodMascota)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codMascota");
            entity.Property(e => e.FechaConsulta).HasColumnName("fechaConsulta");
            entity.Property(e => e.Diagnostico)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("diagnostico");
            entity.Property(e => e.Medicacion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("medicacion");
            entity.Property(e => e.Motivo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("motivo");
            entity.Property(e => e.Tratamiento)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("tratamiento");

            entity.HasOne(d => d.CodMascotaNavigation).WithMany(p => p.Consulta)
                .HasForeignKey(d => d.CodMascota)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MascConsultas");
        });


        modelBuilder.Entity<ConsumoHotel>(entity =>
        {
            entity.HasKey(e => new { e.IdHospedaje, e.IdServicio, e.CodMascota });

            entity.ToTable("ConsumoHotel");

            entity.Property(e => e.IdHospedaje).HasColumnName("idHospedaje");
            entity.Property(e => e.IdServicio)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idServicio");
            entity.Property(e => e.CodMascota)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codMascota");
            entity.Property(e => e.CantidadAlim).HasColumnName("cantidadAlim");
            entity.Property(e => e.CantidadCom).HasColumnName("cantidadCom");
            entity.Property(e => e.CantidadBanos).HasDefaultValue(0);
            entity.Property(e => e.CantidadMedic).HasColumnName("cantidadMedic");
            entity.Property(e => e.CodAlimento).HasColumnName("codAlimento");
            entity.Property(e => e.CodMedicamento).HasColumnName("codMedicamento");
            entity.Property(e => e.IdComodidad).HasColumnName("idComodidad");
            entity.Property(e => e.Nit)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NIT");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("observaciones");
            entity.Property(e => e.NochesHosp).HasColumnName("nochesHosp");

            entity.HasOne(d => d.CodAlimentoNavigation).WithMany(p => p.ConsumoHotels)
                .HasForeignKey(d => d.CodAlimento)
                .HasConstraintName("FK_AlimCH");

            entity.HasOne(d => d.CodMascotaNavigation).WithMany(p => p.ConsumoHotels)
                .HasForeignKey(d => d.CodMascota)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MascCH");

            entity.HasOne(d => d.CodMedicamentoNavigation).WithMany(p => p.ConsumoHotels)
                .HasForeignKey(d => d.CodMedicamento)
                .HasConstraintName("FK_MedicCH");

            entity.HasOne(d => d.IdComodidadNavigation).WithMany(p => p.ConsumoHotels)
                .HasForeignKey(d => d.IdComodidad)
                .HasConstraintName("FK_ComodCH");

            entity.HasOne(d => d.IdHospedajeNavigation).WithMany(p => p.ConsumoHotels)
                .HasForeignKey(d => new { d.IdHospedaje, d.CodMascota })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HospedajeCH");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.ConsumoHotels)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiciosCH");
        });


        modelBuilder.Entity<ConsumosVet>(entity =>
        {
            entity.HasKey(e => new { e.CodMascota, e.IdServicio, e.IdConsumoVet }).HasName("PK_CV");

            entity.ToTable("ConsumosVet");

            entity.Property(e => e.CodMascota)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codMascota");
            entity.Property(e => e.IdServicio)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idServicio");
            entity.Property(e => e.IdConsumoVet)
                .ValueGeneratedOnAdd()
                .HasColumnName("idConsumoVet");
            entity.Property(e => e.CantVacunas).HasColumnName("cantVacunas");
            entity.Property(e => e.CodVacuna)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codVacuna");
            entity.Property(e => e.Nit)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("nit");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("observaciones");

            entity.HasOne(d => d.CodMascotaNavigation).WithMany(p => p.ConsumosVets)
                .HasForeignKey(d => d.CodMascota)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MasCV");

            entity.HasOne(d => d.CodVacunaNavigation).WithMany(p => p.ConsumosVets)
                .HasForeignKey(d => d.CodVacuna)
                .HasConstraintName("FK_VacCV");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.ConsumosVets)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SerCV");
        });

        modelBuilder.Entity<HistPeso>(entity =>
        {
            entity.HasKey(e => new { e.CodMascota, e.FechaPesaje }).HasName("PK_HistPeso");

            entity.Property(e => e.CodMascota)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codMascota");
            entity.Property(e => e.FechaPesaje).HasColumnName("fechaPesaje");
            entity.Property(e => e.Peso)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("peso");

            entity.HasOne(d => d.CodMascotaNavigation).WithMany(p => p.HistPesos)
                .HasForeignKey(d => d.CodMascota)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MasHistPesos");
        });

        modelBuilder.Entity<Hospedaje>(entity =>
        {
            entity.HasKey(e => new { e.IdHospedaje, e.CodMascota }).HasName("PK_idHospedaje");

            entity.Property(e => e.IdHospedaje).HasColumnName("idHospedaje");
            entity.Property(e => e.CodMascota)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codMascota");
            entity.Property(e => e.FechaIngreso)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("fechaIngreso");
            entity.Property(e => e.FechaSalida)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("fechaSalida");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("observaciones");

            entity.HasOne(d => d.CodMascotaNavigation).WithMany(p => p.Hospedajes)
                .HasForeignKey(d => d.CodMascota)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MasHosp");
        });


        modelBuilder.Entity<Mascota>(entity =>
        {
            entity.HasKey(e => e.CodMascota);

            entity.Property(e => e.CodMascota)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codMascota");
            entity.Property(e => e.CodCliente)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codCliente");
            entity.Property(e => e.Color)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.Especie)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("especie");
            entity.Property(e => e.FechaNac).HasColumnName("fechaNac");
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Raza)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("raza");

            entity.HasOne(d => d.CodClienteNavigation).WithMany(p => p.Mascota)
                .HasForeignKey(d => d.CodCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CliMascotas");
        });

        modelBuilder.Entity<Medicamento>(entity =>
        {
            entity.HasKey(e => e.CodMedicamento).HasName("PK_Medic");

            entity.Property(e => e.CodMedicamento).HasColumnName("codMedicamento");
            entity.Property(e => e.Laboratorio)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("laboratorio");
            entity.Property(e => e.Presentacion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("presentacion");
            entity.Property(e => e.PesoNeto)
                .HasColumnType("decimal(5,2)")
                .HasColumnName("pesoNeto");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("money")
                .HasColumnName("precioUnitario");
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });


        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Ci);

            entity.Property(e => e.Ci)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ci");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });


        modelBuilder.Entity<PersonaCliente>(entity =>
        {
            entity.HasKey(e => new { e.CodCliente, e.Ci, e.FechaAsociacion }).HasName("PK_PC");

            entity.ToTable("PersonaCliente");

            entity.Property(e => e.CodCliente)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codCliente");
            entity.Property(e => e.Ci)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ci");
            entity.Property(e => e.FechaAsociacion).HasColumnName("fechaAsociacion");

            entity.HasOne(d => d.CiNavigation).WithMany(p => p.PersonaClientes)
                .HasForeignKey(d => d.Ci)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PersonasPC");

            entity.HasOne(d => d.CodClienteNavigation).WithMany(p => p.PersonaClientes)
                .HasForeignKey(d => d.CodCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClientesPC");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PK_Ser");

            entity.Property(e => e.IdServicio)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idServicio");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("money")
                .HasColumnName("precio");
            entity.Property(e => e.TipoServicio)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipoServicio");
            entity.Property(e => e.UnidadMedida)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("unidadMedida");
        });

        modelBuilder.Entity<Vacuna>(entity =>
        {
            entity.HasKey(e => e.CodVacuna).HasName("PK_Vac");

            entity.Property(e => e.CodVacuna)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codVacuna");
            entity.Property(e => e.Dosis)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("dosis");
            entity.Property(e => e.Laboratorio)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("laboratorio");
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("money")
                .HasColumnName("precioUnitario");
            entity.Property(e => e.PrevEnfermedad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("prevEnfermedad");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public void InsertPersonaCliente(string codCliente, string ci, DateOnly fechaAsociacion)
    {
        var codClienteParam = new SqlParameter("@CodCliente", SqlDbType.NVarChar) { Value = codCliente };
        var ciParam = new SqlParameter("@Ci", SqlDbType.NVarChar) { Value = ci };
        var fechaAsociacionParam = new SqlParameter("@FechaAsociacion", SqlDbType.Date) { Value = fechaAsociacion };

        Database.ExecuteSqlRaw("EXEC PersonaCliente_Insert @CodCliente, @Ci, @FechaAsociacion", codClienteParam, ciParam, fechaAsociacionParam);
    }

    public void UpdatePersonaCliente(string codCliente, string ci, DateOnly fechaAsociacion)
    {
        var codClienteParam = new SqlParameter("@CodCliente", SqlDbType.NVarChar) { Value = codCliente };
        var ciParam = new SqlParameter("@Ci", SqlDbType.NVarChar) { Value = ci };
        var fechaAsociacionParam = new SqlParameter("@FechaAsociacion", SqlDbType.Date) { Value = fechaAsociacion };

        Database.ExecuteSqlRaw("EXEC PersonaCliente_Update @CodCliente, @Ci, @FechaAsociacion", codClienteParam, ciParam, fechaAsociacionParam);
    }

    public void DeletePersonaCliente(string codCliente, string ci)
    {
        var codClienteParam = new SqlParameter("@CodCliente", SqlDbType.NVarChar) { Value = codCliente };
        var ciParam = new SqlParameter("@Ci", SqlDbType.NVarChar) { Value = ci };

        Database.ExecuteSqlRaw("EXEC PersonaCliente_Delete @CodCliente, @Ci", codClienteParam, ciParam);
    }

    public void InsertPersona(string ci, string nombre, string telefono, string correo, string direccion)
    {
        var parameters = new[]
        {
            new SqlParameter("@Ci", SqlDbType.NVarChar) { Value = ci },
            new SqlParameter("@Nombre", SqlDbType.NVarChar) { Value = nombre },
            new SqlParameter("@Telefono", SqlDbType.NVarChar) { Value = telefono },
            new SqlParameter("@Correo", SqlDbType.NVarChar) { Value = correo },
            new SqlParameter("@Direccion", SqlDbType.NVarChar) { Value = direccion }
        };

        Database.ExecuteSqlRaw("EXEC InsertPersona @Ci, @Nombre, @Telefono, @Correo, @Direccion", parameters);
    }

    public void UpdatePersona(string ci, string nombre, string telefono, string correo, string direccion)
    {
        var parameters = new[]
        {
            new SqlParameter("@Ci", SqlDbType.NVarChar) { Value = ci },
            new SqlParameter("@Nombre", SqlDbType.NVarChar) { Value = nombre },
            new SqlParameter("@Telefono", SqlDbType.NVarChar) { Value = telefono },
            new SqlParameter("@Correo", SqlDbType.NVarChar) { Value = correo },
            new SqlParameter("@Direccion", SqlDbType.NVarChar) { Value = direccion }
        };

        Database.ExecuteSqlRaw("EXEC UpdatePersona @Ci, @Nombre, @Telefono, @Correo, @Direccion", parameters);
    }

    public void DeletePersona(string ci)
    {
        var ciParam = new SqlParameter("@Ci", SqlDbType.NVarChar) { Value = ci };

        Database.ExecuteSqlRaw("EXEC DeletePersona @Ci", ciParam);
    }

    public void InsertCliente(string codCliente, string apellido, string banco, string correo, string cuentaBanco, string direccion, string telefono)
    {
        var parameters = new[]
        {
        new SqlParameter("@CodCliente", SqlDbType.NVarChar) { Value = codCliente },
        new SqlParameter("@Apellido", SqlDbType.NVarChar) { Value = apellido },
        new SqlParameter("@Banco", SqlDbType.NVarChar) { Value = banco },
        new SqlParameter("@Correo", SqlDbType.NVarChar) { Value = correo },
        new SqlParameter("@CuentaBanco", SqlDbType.NVarChar) { Value = cuentaBanco },
        new SqlParameter("@Direccion", SqlDbType.NVarChar) { Value = direccion },
        new SqlParameter("@Telefono", SqlDbType.NVarChar) { Value = telefono }
    };

        Database.ExecuteSqlRaw("EXEC InsertCliente @CodCliente, @Apellido, @Banco, @Correo, @CuentaBanco, @Direccion, @Telefono", parameters);
    }

    public void UpdateCliente(string codCliente, string apellido, string banco, string correo, string cuentaBanco, string direccion, string telefono)
    {
        var parameters = new[]
        {
        new SqlParameter("@CodCliente", SqlDbType.NVarChar) { Value = codCliente },
        new SqlParameter("@Apellido", SqlDbType.NVarChar) { Value = apellido },
        new SqlParameter("@Banco", SqlDbType.NVarChar) { Value = banco },
        new SqlParameter("@Correo", SqlDbType.NVarChar) { Value = correo },
        new SqlParameter("@CuentaBanco", SqlDbType.NVarChar) { Value = cuentaBanco },
        new SqlParameter("@Direccion", SqlDbType.NVarChar) { Value = direccion },
        new SqlParameter("@Telefono", SqlDbType.NVarChar) { Value = telefono }
    };

        Database.ExecuteSqlRaw("EXEC UpdateCliente @CodCliente, @Apellido, @Banco, @Correo, @CuentaBanco, @Direccion, @Telefono", parameters);
    }

    public void DeleteCliente(string codCliente)
    {
        var codClienteParam = new SqlParameter("@CodCliente", SqlDbType.NVarChar) { Value = codCliente };

        Database.ExecuteSqlRaw("EXEC DeleteCliente @CodCliente", codClienteParam);
    }

    public void InsertVacuna(string codVacuna, string nombre, string laboratorio, string prevEnfermedad, decimal dosis, decimal precioUnitario)
    {
        var parameters = new[]
        {
                new SqlParameter("@CodVacuna", SqlDbType.NVarChar) { Value = codVacuna },
                new SqlParameter("@Nombre", SqlDbType.NVarChar) { Value = nombre },
                new SqlParameter("@Laboratorio", SqlDbType.NVarChar) { Value = laboratorio },
                new SqlParameter("@PrevEnfermedad", SqlDbType.NVarChar) { Value = prevEnfermedad },
                new SqlParameter("@Dosis", SqlDbType.Decimal) { Value = dosis },
                new SqlParameter("@PrecioUnitario", SqlDbType.Decimal) { Value = precioUnitario }
            };

        Database.ExecuteSqlRaw("EXEC InsertVacuna @CodVacuna, @Nombre, @Laboratorio, @PrevEnfermedad, @Dosis, @PrecioUnitario", parameters);
    }

    public void UpdateVacuna(string codVacuna, string nombre, string laboratorio, string prevEnfermedad, decimal dosis, decimal precioUnitario)
    {
        var parameters = new[]
        {
                new SqlParameter("@CodVacuna", SqlDbType.NVarChar) { Value = codVacuna },
                new SqlParameter("@Nombre", SqlDbType.NVarChar) { Value = nombre },
                new SqlParameter("@Laboratorio", SqlDbType.NVarChar) { Value = laboratorio },
                new SqlParameter("@PrevEnfermedad", SqlDbType.NVarChar) { Value = prevEnfermedad },
                new SqlParameter("@Dosis", SqlDbType.Decimal) { Value = dosis },
                new SqlParameter("@PrecioUnitario", SqlDbType.Decimal) { Value = precioUnitario }
            };

        Database.ExecuteSqlRaw("EXEC UpdateVacuna @CodVacuna, @Nombre, @Laboratorio, @PrevEnfermedad, @Dosis, @PrecioUnitario", parameters);
    }

    public void DeleteVacuna(string codVacuna)
    {
        var codVacunaParam = new SqlParameter("@CodVacuna", SqlDbType.NVarChar) { Value = codVacuna };

        Database.ExecuteSqlRaw("EXEC DeleteVacuna @CodVacuna", codVacunaParam);
    }

    public void InsertMascota(string codMascota, string codCliente, string nombre, string especie, string raza, string color, DateTime? fechaNac)
    {
        var parameters = new[]
        {
            new SqlParameter("@CodMascota", SqlDbType.NVarChar) { Value = codMascota },
            new SqlParameter("@CodCliente", SqlDbType.NVarChar) { Value = codCliente },
            new SqlParameter("@Nombre", SqlDbType.NVarChar) { Value = nombre },
            new SqlParameter("@Especie", SqlDbType.NVarChar) { Value = especie },
            new SqlParameter("@Raza", SqlDbType.NVarChar) { Value = raza },
            new SqlParameter("@Color", SqlDbType.NVarChar) { Value = color },
            new SqlParameter("@FechaNac", SqlDbType.Date) { Value = (object)fechaNac ?? DBNull.Value }
        };

        Database.ExecuteSqlRaw("EXEC InsertMascota @CodMascota, @CodCliente, @Nombre, @Especie, @Raza, @Color, @FechaNac", parameters);
    }

    public void UpdateMascota(string codMascota, string codCliente, string nombre, string especie, string raza, string color, DateTime? fechaNac)
    {
        var parameters = new[]
        {
            new SqlParameter("@CodMascota", SqlDbType.NVarChar) { Value = codMascota },
            new SqlParameter("@CodCliente", SqlDbType.NVarChar) { Value = codCliente },
            new SqlParameter("@Nombre", SqlDbType.NVarChar) { Value = nombre },
            new SqlParameter("@Especie", SqlDbType.NVarChar) { Value = especie },
            new SqlParameter("@Raza", SqlDbType.NVarChar) { Value = raza },
            new SqlParameter("@Color", SqlDbType.NVarChar) { Value = color },
            new SqlParameter("@FechaNac", SqlDbType.Date) { Value = (object)fechaNac ?? DBNull.Value }
        };

        Database.ExecuteSqlRaw("EXEC UpdateMascota @CodMascota, @CodCliente, @Nombre, @Especie, @Raza, @Color, @FechaNac", parameters);
    }

    public void DeleteMascota(string codMascota)
    {
        var codMascotaParam = new SqlParameter("@CodMascota", SqlDbType.NVarChar) { Value = codMascota };

        Database.ExecuteSqlRaw("EXEC DeleteMascota @CodMascota", codMascotaParam);
    }
    public async Task InsertarConsulta(string codMascota, DateOnly fechaConsulta, string motivo, string diagnostico, string tratamiento, string medicacion)
    {
        await Database.ExecuteSqlRawAsync("EXEC InsertarConsulta @p0, @p1, @p2, @p3, @p4, @p5",
            parameters: new object[] { codMascota, fechaConsulta.ToString("yyyy-MM-dd"), motivo, diagnostico, tratamiento, medicacion });
    }

    public async Task ActualizarConsulta(string codMascota, DateOnly fechaConsulta, string motivo, string diagnostico, string tratamiento, string medicacion)
    {
        await Database.ExecuteSqlRawAsync("EXEC ActualizarConsulta @p0, @p1, @p2, @p3, @p4, @p5",
            parameters: new object[] { codMascota, fechaConsulta.ToString("yyyy-MM-dd"), motivo, diagnostico, tratamiento, medicacion });
    }

    public async Task BorrarConsulta(string codMascota, DateOnly fechaConsulta)
    {
        await Database.ExecuteSqlRawAsync("EXEC BorrarConsulta @p0, @p1",
            parameters: new object[] { codMascota, fechaConsulta.ToString("yyyy-MM-dd") });
    }

    public void InsertarAplicaVacuna(string codMascota, string codVacuna, DateOnly fechaPrevista, DateOnly? fechaAplicacion, int dosisAplicada)
    {
        var codMascotaParam = new SqlParameter("@CodMascota", SqlDbType.NVarChar) { Value = codMascota };
        var codVacunaParam = new SqlParameter("@CodVacuna", SqlDbType.NVarChar) { Value = codVacuna };
        var fechaPrevistaParam = new SqlParameter("@FechaPrevista", SqlDbType.Date) { Value = fechaPrevista };
        var fechaAplicacionParam = new SqlParameter("@FechaAplicacion", SqlDbType.Date) { Value = (object)fechaAplicacion ?? DBNull.Value };
        var dosisAplicadaParam = new SqlParameter("@DosisAplicada", SqlDbType.Int) { Value = dosisAplicada };

        Database.ExecuteSqlRaw("EXEC InsertarAplicaVacuna @CodMascota, @CodVacuna, @FechaPrevista, @FechaAplicacion, @DosisAplicada",
            codMascotaParam, codVacunaParam, fechaPrevistaParam, fechaAplicacionParam, dosisAplicadaParam);
    }

    public void ActualizarAplicaVacuna(string codMascota, string codVacuna, DateOnly fechaPrevista, DateOnly? nuevaFechaAplicacion, int nuevaDosisAplicada)
    {
        var codMascotaParam = new SqlParameter("@CodMascota", SqlDbType.NVarChar) { Value = codMascota };
        var codVacunaParam = new SqlParameter("@CodVacuna", SqlDbType.NVarChar) { Value = codVacuna };
        var fechaPrevistaParam = new SqlParameter("@FechaPrevista", SqlDbType.Date) { Value = fechaPrevista };
        var nuevaFechaAplicacionParam = new SqlParameter("@FechaAplicacion", SqlDbType.Date) { Value = (object)nuevaFechaAplicacion ?? DBNull.Value };
        var nuevaDosisAplicadaParam = new SqlParameter("@DosisAplicada", SqlDbType.Int) { Value = nuevaDosisAplicada };

        Database.ExecuteSqlRaw("EXEC ActualizarAplicaVacuna @CodMascota, @CodVacuna, @FechaPrevista, @FechaAplicacion, @DosisAplicada",
            codMascotaParam, codVacunaParam, fechaPrevistaParam, nuevaFechaAplicacionParam, nuevaDosisAplicadaParam);
    }

    public void BorrarAplicaVacuna(string codMascota, string codVacuna, DateOnly fechaPrevista)
    {
        var codMascotaParam = new SqlParameter("@CodMascota", SqlDbType.NVarChar) { Value = codMascota };
        var codVacunaParam = new SqlParameter("@CodVacuna", SqlDbType.NVarChar) { Value = codVacuna };
        var fechaPrevistaParam = new SqlParameter("@FechaPrevista", SqlDbType.Date) { Value = fechaPrevista };

        Database.ExecuteSqlRaw("EXEC BorrarAplicaVacuna @CodMascota, @CodVacuna, @FechaPrevista",
            codMascotaParam, codVacunaParam, fechaPrevistaParam);
    }

    public async Task RegistrarHospedajeSinExtra(RegistrarHospedajeViewModel model)
    {
        await Database.ExecuteSqlRawAsync("EXEC RegistrarHospedaje @CodMascota, @FechaIngreso, @FechaSalida, @UsaNecesidadesEspeciales, @TamanoMascota",
            new SqlParameter("@CodMascota", model.CodMascota),
            new SqlParameter("@FechaIngreso", model.FechaIngreso),
            new SqlParameter("@FechaSalida", model.FechaSalida),
            new SqlParameter("@UsaNecesidadesEspeciales", model.UsaNecesidadesEspeciales),
            new SqlParameter("@TamanoMascota", model.TamanoMascota));
    }

    public async Task RegistrarHospedajeConExtra(RegistrarHospedajeViewModel model)
    {
        await Database.ExecuteSqlRawAsync("EXEC RegistrarHospedaje @CodMascota, @FechaIngreso, @FechaSalida, @UsaNecesidadesEspeciales, @TamanoMascota, @NombreAlimento, @DescripcionAlimento, @ProveedorAlimento, @CantidadAlimento, @NombreComodidad, @DescripcionComodidad, @CantidadComodidad, @NombreMedicamento, @LaboratorioMedicamento, @PresentacionMedicamento, @PesoNetoMedicamento, @CantidadMedicamento",
            new SqlParameter("@CodMascota", model.CodMascota),
            new SqlParameter("@FechaIngreso", model.FechaIngreso),
            new SqlParameter("@FechaSalida", model.FechaSalida),
            new SqlParameter("@UsaNecesidadesEspeciales", model.UsaNecesidadesEspeciales),
            new SqlParameter("@TamanoMascota", model.TamanoMascota),
            new SqlParameter("@NombreAlimento", model.NombreAlimento),
            new SqlParameter("@DescripcionAlimento", model.DescripcionAlimento),
            new SqlParameter("@ProveedorAlimento", model.ProveedorAlimento),
            new SqlParameter("@CantidadAlimento", model.CantidadAlimento),
            new SqlParameter("@NombreComodidad", model.NombreComodidad),
            new SqlParameter("@DescripcionComodidad", model.DescripcionComodidad),
            new SqlParameter("@CantidadComodidad", model.CantidadComodidad),
            new SqlParameter("@NombreMedicamento", model.NombreMedicamento),
            new SqlParameter("@LaboratorioMedicamento", model.LaboratorioMedicamento),
            new SqlParameter("@PresentacionMedicamento", model.PresentacionMedicamento),
            new SqlParameter("@PesoNetoMedicamento", model.PesoNetoMedicamento),
            new SqlParameter("@CantidadMedicamento", model.CantidadMedicamento));
    }

}

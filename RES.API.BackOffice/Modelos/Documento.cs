﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.IO;

namespace DAES.API.BackOffice.Modelos
{
    [Table("Documento")]
    public class Documento
    {
        public Documento()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Id documento")]
        public int DocumentoId { get; set; }

        [Display(Name = "Organización")]
        public int? OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        [Display(Name = "Workflow")]
        public int? WorkflowId { get; set; }
        public virtual Workflow Workflow { get; set; }

        [Display(Name = "Proceso")]
        public int? ProcesoId { get; set; }
        public virtual Proceso Proceso { get; set; }

        [Display(Name = "Tipo documento")]
        public int TipoDocumentoId { get; set; }
        //public virtual TipoDocumento TipoDocumento { get; set; }

        [Display(Name = "Firmante")]
        public int? FirmanteId { get; set; }
        //public virtual Firmante Firmante { get; set; }

        [Display(Name = "Enviado?")]
        public bool? Enviado { get; set; } = false;

        [Display(Name = "Fecha creación")]
        public DateTime? FechaCreacion { get; set; } = DateTime.Now;

        [Display(Name = "Válido hasta")]
        public DateTime? FechaValidoHasta { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        [Display(Name = "Autor")]
        public string Autor { get; set; }

        [Display(Name = "Nombre")]
        public string FileName { get; set; }

        [Display(Name = "Contenido")]
        public byte[] Content { get; set; }

        [Display(Name = "Texto recordatorio")]
        [DataType(DataType.MultilineText)]
        public string Recordatorio { get; set; }

        [Display(Name = "Fecha recordatorio")]
        public DateTime? FechaRecordatorio { get; set; }

        [Display(Name = "Fecha resolución")]
        public DateTime? FechaResolucion { get; set; }

        [Display(Name = "Resuelto?")]
        public bool Resuelto { get; set; } = false;

        [Display(Name = "URL origen")]
        public string Url { get; set; }

        [Display(Name = "Firmado")]
        public bool Firmado { get; set; } = false;

        [Display(Name = "Activo")]
        public bool Activo { get; set; } = true;

        public int? CertificadoId { get; set; }

        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Display(Name = "Número oficio")]
        public string NumeroOficio { get; set; }

        [Display(Name = "Fecha salida de oficio")]
        public DateTime? FechaSalidaOficio{ get; set; }

        [Display(Name = "Tipo privacidad")]
        public int TipoPrivacidadId { get; set; }
        //public virtual TipoPrivacidad TipoPrivacidad { get; set; }

        [Display(Name = "Número Folio")]
        public string NumeroFolio { get; set; }

        [Display(Name = "Periodo")]
        public string Periodo { get; set; }
        public Guid? uniqueid { get; set; }

        //Nuevo metodo de firma
        [NotMapped]
        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? Fecha { get; set; } = DateTime.Now;

        [NotMapped]
        [Display(Name = "Autor")]
        public string Email { get; set; }

        //[Display(Name = "Firmado")]
        //public bool Signed { get; set; } = false;

        [Display(Name = "Folio")]
        public string Folio { get; set; }
        [NotMapped]
        public byte[] File { get; set; }

        [NotMapped]
        public string Texto { get; set; }
        [NotMapped]
        public string Metadata { get; set; }
        [NotMapped]
        public string Type { get; set; }
        [NotMapped]
        public byte[] DocumentoSinFirma { get; set; }
        [NotMapped]
        public byte[] DocumentoConFirma { get; set; }
        [NotMapped]
        public string DocumentoConFirmaFilename { get; set; }
        [NotMapped]
        public string DocumentoSinFirmaFilename { get; set; }
        [NotMapped]
        public DateTime? FechaFirma { get; set; }
        [NotMapped]
        public string Firmantes { get; set; }
        [NotMapped]
        public string TipoDocumentoCodigo { get; set; }
        [NotMapped]
        public string Observaciones { get; set; }

        //public string Url { get; set; }
        [NotMapped]
        public string TipoDocumentoDescripcion { get; set; }
    }
}

#nullable disable

namespace MbCore
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.loc.gov/METS/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.loc.gov/METS/", IsNullable = false)]
#pragma warning disable CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.
    public partial class mets
#pragma warning restore CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.
    {
        private metsDmdSec dmdSecField;

        private metsAmdSec amdSecField;

        private metsFileSec fileSecField;

        private metsStructMap[] structMapField;

        /// <remarks/>
        public metsDmdSec dmdSec
        {
            get
            {
                return this.dmdSecField;
            }
            set
            {
                this.dmdSecField = value;
            }
        }

        /// <remarks/>
        public metsAmdSec amdSec
        {
            get
            {
                return this.amdSecField;
            }
            set
            {
                this.amdSecField = value;
            }
        }

        /// <remarks/>
        public metsFileSec fileSec
        {
            get
            {
                return this.fileSecField;
            }
            set
            {
                this.fileSecField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("structMap")]
        public metsStructMap[] structMap
        {
            get
            {
                return this.structMapField;
            }
            set
            {
                this.structMapField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.loc.gov/METS/")]
    public partial class metsDmdSec
    {

        private metsDmdSecMdWrap mdWrapField;

        private string idField;

        /// <remarks/>
        public metsDmdSecMdWrap mdWrap
        {
            get
            {
                return this.mdWrapField;
            }
            set
            {
                this.mdWrapField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.loc.gov/METS/")]
    public partial class metsDmdSecMdWrap
    {

        private metsDmdSecMdWrapXmlData xmlDataField;

        private string mIMETYPEField;

        private string mDTYPEField;

        /// <remarks/>
        public metsDmdSecMdWrapXmlData xmlData
        {
            get
            {
                return this.xmlDataField;
            }
            set
            {
                this.xmlDataField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MIMETYPE
        {
            get
            {
                return this.mIMETYPEField;
            }
            set
            {
                this.mIMETYPEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MDTYPE
        {
            get
            {
                return this.mDTYPEField;
            }
            set
            {
                this.mDTYPEField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.loc.gov/METS/")]
    public partial class metsDmdSecMdWrapXmlData
    {

        private Mods modsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.loc.gov/mods/v3")]
        public Mods mods
        {
            get
            {
                return this.modsField;
            }
            set
            {
                this.modsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.loc.gov/mods/v3")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.loc.gov/mods/v3", IsNullable = false)]
    public partial class Mods
    {

        private modsRelatedItem relatedItemField;

        private modsLocation locationField;

        private modsOriginInfo originInfoField;

        private modsTitleInfo titleInfoField;

        private modsIdentifier[] identifierField;

        private decimal versionField;

        /// <remarks/>
        public modsRelatedItem relatedItem
        {
            get
            {
                return this.relatedItemField;
            }
            set
            {
                this.relatedItemField = value;
            }
        }

        /// <remarks/>
        public modsLocation location
        {
            get
            {
                return this.locationField;
            }
            set
            {
                this.locationField = value;
            }
        }

        /// <remarks/>
        public modsOriginInfo originInfo
        {
            get
            {
                return this.originInfoField;
            }
            set
            {
                this.originInfoField = value;
            }
        }

        /// <remarks/>
        public modsTitleInfo titleInfo
        {
            get
            {
                return this.titleInfoField;
            }
            set
            {
                this.titleInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("identifier")]
        public modsIdentifier[] identifier
        {
            get
            {
                return this.identifierField;
            }
            set
            {
                this.identifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.loc.gov/mods/v3")]
    public partial class modsRelatedItem
    {

        private modsRelatedItemTitleInfo titleInfoField;

        private string typeField;

        /// <remarks/>
        public modsRelatedItemTitleInfo titleInfo
        {
            get
            {
                return this.titleInfoField;
            }
            set
            {
                this.titleInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.loc.gov/mods/v3")]
    public partial class modsRelatedItemTitleInfo
    {

        private string titleField;

        /// <remarks/>
        public string title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.loc.gov/mods/v3")]
    public partial class modsLocation
    {

        private string shelfLocatorField;

        /// <remarks/>
        public string shelfLocator
        {
            get
            {
                return this.shelfLocatorField;
            }
            set
            {
                this.shelfLocatorField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.loc.gov/mods/v3")]
    public partial class modsOriginInfo
    {

        private modsOriginInfoDateCreated[] dateCreatedField;

        private string eventTypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("dateCreated")]
        public modsOriginInfoDateCreated[] dateCreated
        {
            get
            {
                return this.dateCreatedField;
            }
            set
            {
                this.dateCreatedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string eventType
        {
            get
            {
                return this.eventTypeField;
            }
            set
            {
                this.eventTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.loc.gov/mods/v3")]
    public partial class modsOriginInfoDateCreated
    {

        private string ncodingField;

        private string keyDateField;

        private string pointField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ncoding
        {
            get
            {
                return this.ncodingField;
            }
            set
            {
                this.ncodingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string keyDate
        {
            get
            {
                return this.keyDateField;
            }
            set
            {
                this.keyDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string point
        {
            get
            {
                return this.pointField;
            }
            set
            {
                this.pointField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.loc.gov/mods/v3")]
    public partial class modsTitleInfo
    {

        private string titleField;

        /// <remarks/>
        public string title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.loc.gov/mods/v3")]
    public partial class modsIdentifier
    {

        private string typeField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.loc.gov/METS/")]
    public partial class metsAmdSec
    {

        private metsAmdSecRightsMD rightsMDField;

        private metsAmdSecDigiprovMD digiprovMDField;

        private string idField;

        /// <remarks/>
        public metsAmdSecRightsMD rightsMD
        {
            get
            {
                return this.rightsMDField;
            }
            set
            {
                this.rightsMDField = value;
            }
        }

        /// <remarks/>
        public metsAmdSecDigiprovMD digiprovMD
        {
            get
            {
                return this.digiprovMDField;
            }
            set
            {
                this.digiprovMDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.loc.gov/METS/")]
    public partial class metsAmdSecRightsMD
    {

        private metsAmdSecRightsMDMdWrap mdWrapField;

        private string idField;

        /// <remarks/>
        public metsAmdSecRightsMDMdWrap mdWrap
        {
            get
            {
                return this.mdWrapField;
            }
            set
            {
                this.mdWrapField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.loc.gov/METS/")]
    public partial class metsAmdSecRightsMDMdWrap
    {

        private metsAmdSecRightsMDMdWrapXmlData xmlDataField;

        private string mIMETYPEField;

        private string mDTYPEField;

        private string oTHERMDTYPEField;

        /// <remarks/>
        public metsAmdSecRightsMDMdWrapXmlData xmlData
        {
            get
            {
                return this.xmlDataField;
            }
            set
            {
                this.xmlDataField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MIMETYPE
        {
            get
            {
                return this.mIMETYPEField;
            }
            set
            {
                this.mIMETYPEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MDTYPE
        {
            get
            {
                return this.mDTYPEField;
            }
            set
            {
                this.mDTYPEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string OTHERMDTYPE
        {
            get
            {
                return this.oTHERMDTYPEField;
            }
            set
            {
                this.oTHERMDTYPEField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.loc.gov/METS/")]
    public partial class metsAmdSecRightsMDMdWrapXmlData
    {

        private Rights rightsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://dfg-viewer.de/")]
        public Rights rights
        {
            get
            {
                return this.rightsField;
            }
            set
            {
                this.rightsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://dfg-viewer.de/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://dfg-viewer.de/", IsNullable = false)]
    public partial class Rights
    {

        private string ownerField;

        private string ownerContactField;

        private string ownerSiteURLField;

        /// <remarks/>
        public string owner
        {
            get
            {
                return this.ownerField;
            }
            set
            {
                this.ownerField = value;
            }
        }

        /// <remarks/>
        public string ownerContact
        {
            get
            {
                return this.ownerContactField;
            }
            set
            {
                this.ownerContactField = value;
            }
        }

        /// <remarks/>
        public string ownerSiteURL
        {
            get
            {
                return this.ownerSiteURLField;
            }
            set
            {
                this.ownerSiteURLField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.loc.gov/METS/")]
    public partial class metsAmdSecDigiprovMD
    {

        private metsAmdSecDigiprovMDMdWrap mdWrapField;

        private string idField;

        /// <remarks/>
        public metsAmdSecDigiprovMDMdWrap mdWrap
        {
            get
            {
                return this.mdWrapField;
            }
            set
            {
                this.mdWrapField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.loc.gov/METS/")]
    public partial class metsAmdSecDigiprovMDMdWrap
    {

        private metsAmdSecDigiprovMDMdWrapXmlData xmlDataField;

        private string mIMETYPEField;

        private string mDTYPEField;

        private string oTHERMDTYPEField;

        /// <remarks/>
        public metsAmdSecDigiprovMDMdWrapXmlData xmlData
        {
            get
            {
                return this.xmlDataField;
            }
            set
            {
                this.xmlDataField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MIMETYPE
        {
            get
            {
                return this.mIMETYPEField;
            }
            set
            {
                this.mIMETYPEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MDTYPE
        {
            get
            {
                return this.mDTYPEField;
            }
            set
            {
                this.mDTYPEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string OTHERMDTYPE
        {
            get
            {
                return this.oTHERMDTYPEField;
            }
            set
            {
                this.oTHERMDTYPEField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.loc.gov/METS/")]
    public partial class metsAmdSecDigiprovMDMdWrapXmlData
    {

        private Links linksField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://dfg-viewer.de/")]
        public Links links
        {
            get
            {
                return this.linksField;
            }
            set
            {
                this.linksField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://dfg-viewer.de/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://dfg-viewer.de/", IsNullable = false)]
    public partial class Links
    {

        private string referenceField;

        private string presentationField;

        /// <remarks/>
        public string reference
        {
            get
            {
                return this.referenceField;
            }
            set
            {
                this.referenceField = value;
            }
        }

        /// <remarks/>
        public string presentation
        {
            get
            {
                return this.presentationField;
            }
            set
            {
                this.presentationField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.loc.gov/METS/")]
    public partial class metsFileSec
    {

        private metsFileSecFileGrp fileGrpField;

        /// <remarks/>
        public metsFileSecFileGrp fileGrp
        {
            get
            {
                return this.fileGrpField;
            }
            set
            {
                this.fileGrpField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.loc.gov/METS/")]
    public partial class metsFileSecFileGrp
    {

        private metsFileSecFileGrpFile[] fileField;

        private string uSEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("file")]
        public metsFileSecFileGrpFile[] file
        {
            get
            {
                return this.fileField;
            }
            set
            {
                this.fileField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string USE
        {
            get
            {
                return this.uSEField;
            }
            set
            {
                this.uSEField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.loc.gov/METS/")]
    public partial class metsFileSecFileGrpFile
    {

        private metsFileSecFileGrpFileFLocat fLocatField;

        private string idField;

        private string mIMETYPEField;

        /// <remarks/>
        public metsFileSecFileGrpFileFLocat FLocat
        {
            get
            {
                return this.fLocatField;
            }
            set
            {
                this.fLocatField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MIMETYPE
        {
            get
            {
                return this.mIMETYPEField;
            }
            set
            {
                this.mIMETYPEField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.loc.gov/METS/")]
    public partial class metsFileSecFileGrpFileFLocat
    {

        private string lOCTYPEField;

        private string hrefField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LOCTYPE
        {
            get
            {
                return this.lOCTYPEField;
            }
            set
            {
                this.lOCTYPEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/1999/xlink")]
        public string href
        {
            get
            {
                return this.hrefField;
            }
            set
            {
                this.hrefField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.loc.gov/METS/")]
    public partial class metsStructMap
    {

        private metsStructMapDiv divField;

        private string tYPEField;

        /// <remarks/>
        public metsStructMapDiv div
        {
            get
            {
                return this.divField;
            }
            set
            {
                this.divField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TYPE
        {
            get
            {
                return this.tYPEField;
            }
            set
            {
                this.tYPEField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.loc.gov/METS/")]
    public partial class metsStructMapDiv
    {

        private metsStructMapDivDiv[] divField;

        private string idField;

        private string tYPEField;

        private string lABELField;

        private string dMDIDField;

        private string aDMIDField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("div")]
        public metsStructMapDivDiv[] div
        {
            get
            {
                return this.divField;
            }
            set
            {
                this.divField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TYPE
        {
            get
            {
                return this.tYPEField;
            }
            set
            {
                this.tYPEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LABEL
        {
            get
            {
                return this.lABELField;
            }
            set
            {
                this.lABELField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DMDID
        {
            get
            {
                return this.dMDIDField;
            }
            set
            {
                this.dMDIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ADMID
        {
            get
            {
                return this.aDMIDField;
            }
            set
            {
                this.aDMIDField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.loc.gov/METS/")]
    public partial class metsStructMapDivDiv
    {

        private metsStructMapDivDivFptr fptrField;

        private string idField;

        private int oRDERField;

        private string tYPEField;

        /// <remarks/>
        public metsStructMapDivDivFptr fptr
        {
            get
            {
                return this.fptrField;
            }
            set
            {
                this.fptrField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int ORDER
        {
            get
            {
                return this.oRDERField;
            }
            set
            {
                this.oRDERField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TYPE
        {
            get
            {
                return this.tYPEField;
            }
            set
            {
                this.tYPEField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.loc.gov/METS/")]
    public partial class metsStructMapDivDivFptr
    {

        private string fILEIDField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string FILEID
        {
            get
            {
                return this.fILEIDField;
            }
            set
            {
                this.fILEIDField = value;
            }
        }
    }
}
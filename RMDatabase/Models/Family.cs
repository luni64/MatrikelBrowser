using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;

namespace RMDatabase.Models;

public partial class Family
{
    #region Properties ----------------------------------------------
    public long FamilyId { get; set; }
    public long HusbandId { get; set; }
    public long WifeId { get; set; }
    public long ChildId { get; set; }
    public long HusbOrder { get; set; }
    public long WifeOrder { get; set; }
    public bool IsPrivate { get; set; } = false;
    public long Proof { get; set; }
    public long SpouseLabel { get; set; }
    public long FatherLabel { get; set; }
    public long MotherLabel { get; set; }
    public string SpouseLabelStr { get; set; } = string.Empty;
    public string FatherLabelStr { get; set; } = string.Empty;
    public string MotherLabelStr { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
    public DateTime ChangeDate { get; set; } = DateTime.Now;
    #endregion

    #region Navigation ----------------------------------------------
    public virtual Person Husband { get; set; } = null!;
    public virtual Person Wife { get; set; } = null!;
    public virtual ICollection<ChildInfo> ChildInfos { get; set; } = [];
    public virtual ICollection<Citation> Citations { get; set; } = null!;
    public virtual ICollection<FamilyEvent> Events { get; set; } = [];
    public virtual ICollection<Task> Tasks { get; set; } = [];
    #endregion

    #region Helpers
    public ChildInfo AddChild(Person child, RelationShip relFather = RelationShip.Birth, RelationShip relMother = RelationShip.Birth)
    {
        var ci = new ChildInfo
        {
            Family = this,
            Child = child,
            RelFather = relFather,
            RelMother = relMother
        };
        ChildInfos.Add(ci);
        return ci;
    }

    #endregion
    public override string ToString() => $"{Husband?.PrimaryName.Surname} {Husband?.PrimaryName.Given} & {Wife?.PrimaryName.Surname} {Wife?.PrimaryName.Given}";
}


public partial class Person
{
    public override string ToString() => $"{PrimaryName.Surname} {PrimaryName.Given}";
}
/*This code is a generated one , Change the source code of the generator if you want some change in this code
You can find the source code of the code generator from here -> https://github.com/rusith/MyCodeGenerator*/

using System;
using System.Collections.Generic;
using System.Linq;
using IndicoPacking.DAL.Base.Implementation;

namespace IndicoPacking.DAL.Objects.Implementation
{
    /// <summary>
    /// This class represents the ShipmentMode table in the database 
    /// </summary>
	public class ShipmentModeBo : Entity
	{
		#region Fields
		
		private int _iD;
		private string _name;
		private string _description;
		private int? _indicoShipmentModeId;

		#endregion

		#region Properties
		
		public int ID { get { return _iD; } set { _iD = value; PrimaryKey = value; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ID"); }}}
		public string Name { get { return _name; } set { _name = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Name"); }}}
		public string Description { get { return _description; } set { _description = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Description"); }}}
		public int? IndicoShipmentModeId { get { return _indicoShipmentModeId; } set { _indicoShipmentModeId = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("IndicoShipmentModeId"); }}}

		#endregion

		#region Methods
				
		public BoCollection<InvoiceBo> InvoicesWhereThisIsShipmentMode()
		{
			 List<InvoiceBo> list;
			 try { list = Context.Unit.InvoiceRepository.Where(new {ShipmentMode = ID}); }catch(Exception){ list = null; } 
			 return Context == null ? null : new BoCollection<InvoiceBo>(this, list, "ShipmentMode");
		}



		#endregion

		#region Internal Methods

		internal override Dictionary<string, object> GetColumns()
        {
            return new Dictionary<string, object> 
			{
				{"ID", ID},
				{"Name", Name},
				{"Description", Description},
				{"IndicoShipmentModeId", IndicoShipmentModeId}
			};
        }

		internal override void Copy(Entity en)
		{
			var entity = en as ShipmentModeBo;
			if(entity == null)
				return;
			
			BusinessObjectState = entity.BusinessObjectState;
			_iD = entity._iD;
			_name = entity._name;
			_description = entity._description;
			_indicoShipmentModeId = entity._indicoShipmentModeId;
		}

		#endregion

		#region Constructors
		
		public ShipmentModeBo()
		{
			TableName = "ShipmentMode";
			PrimaryKeyName = "ID";
		}

		#endregion
	}
} 


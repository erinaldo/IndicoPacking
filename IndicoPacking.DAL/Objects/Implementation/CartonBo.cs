/*This code is a generated one , Change the source code of the generator if you want some change in this code
You can find the source code of the code generator from here -> https://github.com/rusith/MyCodeGenerator*/

using System;
using System.Collections.Generic;
using System.Linq;
using IndicoPacking.DAL.Base.Implementation;

namespace IndicoPacking.DAL.Objects.Implementation
{
    /// <summary>
    /// This class represents the Carton table in the database 
    /// </summary>
	public class CartonBo : Entity
	{
		#region Fields
		
		private int _iD;
		private string _name;
		private int _qty;
		private string _description;

		#endregion

		#region Properties
		
		public int ID { get { return _iD; } set { _iD = value; PrimaryKey = value; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ID"); }}}
		public string Name { get { return _name; } set { _name = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Name"); }}}
		public int Qty { get { return _qty; } set { _qty = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Qty"); }}}
		public string Description { get { return _description; } set { _description = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Description"); }}}

		#endregion

		#region Methods
				
		public BoCollection<ShipmentDetailCartonBo> ShipmentDetailCartonsWhereThisIsCarton()
		{
			 List<ShipmentDetailCartonBo> list;
			 try { list = Context.Unit.ShipmentDetailCartonRepository.Where(new {Carton = ID}); }catch(Exception){ list = null; } 
			 return Context == null ? null : new BoCollection<ShipmentDetailCartonBo>(this, list, "Carton");
		}



		#endregion

		#region Internal Methods

		internal override Dictionary<string, object> GetColumns()
        {
            return new Dictionary<string, object> 
			{
				{"ID", ID},
				{"Name", Name},
				{"Qty", Qty},
				{"Description", Description}
			};
        }

		internal override void Copy(Entity en)
		{
			var entity = en as CartonBo;
			if(entity == null)
				return;
			
			BusinessObjectState = entity.BusinessObjectState;
			_iD = entity._iD;
			_name = entity._name;
			_qty = entity._qty;
			_description = entity._description;
		}

		#endregion

		#region Constructors
		
		public CartonBo()
		{
			TableName = "Carton";
			PrimaryKeyName = "ID";
		}

		#endregion
	}
} 


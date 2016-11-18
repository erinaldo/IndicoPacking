/*This code is a generated one , Change the source code of the generator if you want some change in this code
You can find the source code of the code generator from here -> https://github.com/rusith/MyCodeGenerator*/

using System;
using System.Collections.Generic;
using System.Linq;
using IndicoPacking.DAL.Base.Implementation;

namespace IndicoPacking.DAL.Objects.Implementation
{
    /// <summary>
    /// This class represents the InvoiceStatus table in the database 
    /// </summary>
	public class InvoiceStatusBo : Entity
	{
		#region Fields
		
		private int _iD;
		private string _key;
		private string _name;

		#endregion

		#region Properties
		
		public int ID { get { return _iD; } set { _iD = value; PrimaryKey = value; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ID"); }}}
		public string Key { get { return _key; } set { _key = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Key"); }}}
		public string Name { get { return _name; } set { _name = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Name"); }}}

		#endregion

		#region Methods
				
		public BoCollection<InvoiceBo> InvoicesWhereThisIsInvoiceStatus()
		{
			 List<InvoiceBo> list;
			 try { list = Context.Unit.InvoiceRepository.Where(new {InvoiceStatus = ID}); }catch(Exception){ list = null; } 
			 return Context == null ? null : new BoCollection<InvoiceBo>(this, list, "InvoiceStatus");
		}



		#endregion

		#region Internal Methods

		internal override Dictionary<string, object> GetColumns()
        {
            return new Dictionary<string, object> 
			{
				{"ID", ID},
				{"Key", Key},
				{"Name", Name}
			};
        }

		internal override void Copy(Entity en)
		{
			var entity = en as InvoiceStatusBo;
			if(entity == null)
				return;
			
			BusinessObjectState = entity.BusinessObjectState;
			_iD = entity._iD;
			_key = entity._key;
			_name = entity._name;
		}

		#endregion

		#region Constructors
		
		public InvoiceStatusBo()
		{
			TableName = "InvoiceStatus";
			PrimaryKeyName = "ID";
		}

		#endregion
	}
} 


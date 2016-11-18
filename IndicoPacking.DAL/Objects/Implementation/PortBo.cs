/*This code is a generated one , Change the source code of the generator if you want some change in this code
You can find the source code of the code generator from here -> https://github.com/rusith/MyCodeGenerator*/

using System;
using System.Collections.Generic;
using System.Linq;
using IndicoPacking.DAL.Base.Implementation;

namespace IndicoPacking.DAL.Objects.Implementation
{
    /// <summary>
    /// This class represents the Port table in the database 
    /// </summary>
	public class PortBo : Entity
	{
		#region Fields
		
		private int _iD;
		private string _name;
		private string _description;
		private int? _indicoPortId;

		#endregion

		#region Properties
		
		public int ID { get { return _iD; } set { _iD = value; PrimaryKey = value; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ID"); }}}
		public string Name { get { return _name; } set { _name = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Name"); }}}
		public string Description { get { return _description; } set { _description = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Description"); }}}
		public int? IndicoPortId { get { return _indicoPortId; } set { _indicoPortId = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("IndicoPortId"); }}}

		#endregion

		#region Methods
				
		public BoCollection<DistributorClientAddressBo> DistributorClientAddresssWhereThisIsPort()
		{
			 List<DistributorClientAddressBo> list;
			 try { list = Context.Unit.DistributorClientAddressRepository.Where(new {Port = ID}); }catch(Exception){ list = null; } 
			 return Context == null ? null : new BoCollection<DistributorClientAddressBo>(this, list, "Port");
		}

		
		public BoCollection<InvoiceBo> InvoicesWhereThisIsPort()
		{
			 List<InvoiceBo> list;
			 try { list = Context.Unit.InvoiceRepository.Where(new {Port = ID}); }catch(Exception){ list = null; } 
			 return Context == null ? null : new BoCollection<InvoiceBo>(this, list, "Port");
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
				{"IndicoPortId", IndicoPortId}
			};
        }

		internal override void Copy(Entity en)
		{
			var entity = en as PortBo;
			if(entity == null)
				return;
			
			BusinessObjectState = entity.BusinessObjectState;
			_iD = entity._iD;
			_name = entity._name;
			_description = entity._description;
			_indicoPortId = entity._indicoPortId;
		}

		#endregion

		#region Constructors
		
		public PortBo()
		{
			TableName = "Port";
			PrimaryKeyName = "ID";
		}

		#endregion
	}
} 


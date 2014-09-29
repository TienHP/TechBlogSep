using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace StateMachine{
	[System.Serializable]
	public class ListParameter : NamedParameter {

		private List<object> value;

		public List<object> Value
		{
			get{
				if(!IsConstant){
					ListParameter param=(ListParameter)stateMachine.GetPrameter(Reference);
					if(param== null){
						param=(ListParameter)GlobalParameterCollection.GetParameter(Reference);
					}
					if(param != null){
						return param.value;
					}
				}
				return this.value;
			}
			set{
				this.value = value;
				if(!IsConstant){
					ListParameter param=(ListParameter)stateMachine.GetPrameter(Reference);
					if(param== null){
						param=(ListParameter)GlobalParameterCollection.GetParameter(Reference);
					}
					if(param != null){
						param.value=value;
					}
				}
			}
		}
	}
}
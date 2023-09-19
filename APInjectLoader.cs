using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APInject
{
    /// <summary>
    /// Class that setting class inherits after while setting 
    /// </summary>
    public abstract class APInjectLoader 
    {
        public APInjectLoader()
        {
            _instanceContainers = new List<InstanceContainer>();
        }
        /// <summary>
        /// Overload this method in config class using CreateClassInstancec to fill injector with all used instances. New object created automatically!
        /// </summary>
        public abstract void Load();
        /// <summary>
        /// Creates instance of class and interface storing it internally for further use, returns newly created object. C - class name, I - Interface name. Use unique Desciptor or leave default, args allows to pass parameters to constructors
        /// </summary>
        /// <typeparam name="C"></typeparam>
        /// <typeparam name="I"></typeparam>
        /// <param name="Descriptor"></param>
        /// <param name="args"></param>
        /// <exception cref="Exception"></exception>
        public object CreateClassInstance<C, I>(string Descriptor = "", params object[] args) where C : I
        {
            if(string.IsNullOrEmpty(Descriptor))
            {
                var instance = new InstanceContainer() { ObjectInstance = Activator.CreateInstance(typeof(C), args), ObjectInterface = typeof(I), ObjectType = typeof(C), Description = Descriptor };
                _instanceContainers.Add(instance);
                return instance.ObjectInstance;
            }
            else
            {
                if (_instanceContainers.Any(x => x.Description == Descriptor))
                    throw new Exception($"Object with description {Descriptor} already exist! Use unique descriptors only or empty (default) strings!");
                var instance = new InstanceContainer() { ObjectInstance = Activator.CreateInstance(typeof(C), args), ObjectInterface = typeof(I), ObjectType = typeof(C), Description = Descriptor };
                _instanceContainers.Add(instance);
                return instance.ObjectInstance;
            }
            
        }

        private List<InstanceContainer> _instanceContainers;
        /// <summary>
        /// Clear all bindings (created objects)
        /// </summary>
        public void ClearBindings()
        {
            _instanceContainers?.Clear();   
        }
        /// <summary>
        /// Get instance of class by class type, interface and descriptor (optional). C type of class, I interface of class Descriptor must be unique or default, allowNullObjects if false and instance not found exception is throwed in stead of null
        /// </summary>
        /// <typeparam name="C"></typeparam>
        /// <typeparam name="I"></typeparam>
        /// <param name="Descriptor"></param>
        /// <param name="allowNullObjects"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public I GetInstanceOfClass<C,I>(string Descriptor="", bool allowNullObjects = false) where C : I
        {
            if(string.IsNullOrEmpty(Descriptor))
            {
                if(_instanceContainers.Where(x=>x.ObjectInterface == typeof(I) && x.ObjectType == typeof(C)).Count()>1)
                {
                    var msg = $"Fount more than one assigment of {typeof(I).ToString()}";
                    foreach (var binding in _instanceContainers.Where(x => x.ObjectInterface == typeof(I)))
                        msg += $"to {binding.ObjectType} \n";
                    throw new Exception($"{msg} Try to specify Descriptor to distinguish bindings!");
                }
                else
                {
                    var instance = (I?)_instanceContainers.FirstOrDefault(x => x.ObjectInterface == typeof(I) && x.ObjectType == typeof(C)).ObjectInstance;
                    if (allowNullObjects)
                    {
                        if (instance == null)
                        {
                            throw new Exception($"Object of type {typeof(C)} with interface {typeof(I)} and description {Descriptor} not found!");
                        }
                        else
                            return instance;
                    }
                    else
                        return instance; 
                }
                
            }
            else
            {
                return (I?)_instanceContainers.FirstOrDefault(x => x.Description == Descriptor && x.ObjectInterface == typeof(I) && x.ObjectType == typeof(C)).ObjectInstance;
            }
        }
        /// <summary>
        /// Get instance of class by interface and descriptor (optional). I interface of class Descriptor must be unique or default, allowNullObjects if false and instance not found exception is throwed in stead of null
        /// </summary>
        /// <typeparam name="I"></typeparam>
        /// <param name="Descriptor"></param>
        /// <param name="allowNullObjects"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public I GetInstance<I>(string Descriptor = "", bool allowNullObjects = false)
        {
            if (string.IsNullOrEmpty(Descriptor))
            {
                if (_instanceContainers.Where(x => x.ObjectInterface == typeof(I)).Count() > 1)
                {
                    var msg = $"Fount more than one assigment of {typeof(I).ToString()}";
                    foreach (var binding in _instanceContainers.Where(x => x.ObjectInterface == typeof(I)))
                        msg += $"to {binding.ObjectType} \n";
                    throw new Exception($"{msg} Try to specify Descriptor to distinguish bindings!");
                }
                else
                {
                    var instance = (I?)_instanceContainers.FirstOrDefault(x => x.ObjectInterface == typeof(I)).ObjectInstance;
                    if (allowNullObjects)
                    {
                        if (instance == null)
                        {
                            throw new Exception($"Object of interface {typeof(I)} and description {Descriptor} not found!");
                        }
                        else
                            return instance;
                    }
                    else
                        return instance;
                }

            }
            else
            {
                return (I?)_instanceContainers.FirstOrDefault(x => x.Description == Descriptor && x.ObjectInterface == typeof(I)).ObjectInstance;
            }
        }
    }
}

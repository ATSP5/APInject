namespace APInject
{
    public interface IAPInjectKernel
    {
        /// <summary>
        /// Get instance of class by class type, interface and descriptor (optional). C type of class, I - interface of class Descriptor must be unique or default, allowNullObjects if false and instance not found exception is throwed in stead of null
        /// </summary>
        /// <typeparam name="C"></typeparam>
        /// <typeparam name="I"></typeparam>
        /// <param name="Descriptor"></param>
        /// <param name="allowNullObjects"></param>
        /// <returns></returns>
        I GetInstanceOfClass<C, I>(string Descriptor = "", bool allowNullObjects = false) where C : I;
        /// <summary>
        /// Get instance of class by interface and descriptor (optional). I - interface of class Descriptor must be unique or default, allowNullObjects if false and instance not found exception is throwed in stead of null
        /// </summary>
        /// <typeparam name="I"></typeparam>
        /// <param name="Descriptor"></param>
        /// <param name="allowNullObjects"></param>
        /// <returns></returns>
        I GetInstance<I>(string Descriptor = "", bool allowNullObjects = false);
    }
    public class APInjectKernel : IAPInjectKernel
    {
        APInjectLoader _loader;
        public APInjectKernel(APInjectLoader loader)
        {
            _loader = loader;
            _loader.Load();
        }
       
        public I GetInstanceOfClass<C, I>(string Descriptor = "", bool allowNullObjects = false) where C : I
        {
            return _loader.GetInstanceOfClass<C, I>(Descriptor, allowNullObjects);
        }
       
        public I GetInstance<I>(string Descriptor = "", bool allowNullObjects = false)
        {
            return _loader.GetInstance<I>(Descriptor, allowNullObjects);
        }
    }
}
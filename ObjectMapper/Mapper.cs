using System.Reflection;

namespace ObjectMapper
{
    public class Mapper<TSource, TTarget> where TSource : class, new() where TTarget : class, new()
    {
        class MapProps
        {
            public PropertyInfo Target { get; set; }
            public PropertyInfo Source { get; set; }
        }

        private Dictionary<string, PropertyInfo> targetProps = new Dictionary<string, PropertyInfo>();
        private Dictionary<string, MapProps> matchProps = new Dictionary<string, MapProps>();

        public Mapper()
        {
            var sourceProperties = typeof(TSource).GetProperties();
            var targetProperties = typeof(TTarget).GetProperties();

            foreach (var prop in targetProperties)
            {
                targetProps.Add(prop.Name, prop);
            }

            foreach (var sourceProp in sourceProperties)
            {
                PropertyInfo? targetProp;
                if (targetProps.TryGetValue(sourceProp.Name, out targetProp))
                {
                    matchProps.Add(sourceProp.Name, new MapProps()
                    {
                        Source = sourceProp,
                        Target = targetProp
                    });
                }
            }
        }

        public TTarget MapObject(TSource source)
        {
            var target = new TTarget();

            foreach (var props in matchProps)
            {
                var value = props.Value.Source.GetValue(source);
                props.Value.Target.SetValue(target, value);
            }

            return target;
        }
    }
}
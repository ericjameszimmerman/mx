using mx.core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.json
{
    public class JsonObjectFactory
    {
        public void BuildJsonTreeCollectionFile(Stream stream, TreeCollection collection)
        {
            using (StreamWriter sw = new StreamWriter(stream))
            {
                using (JsonWriter jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = Formatting.Indented;

                    JsonSerializer serializer = new JsonSerializer();

                    serializer.NullValueHandling = NullValueHandling.Ignore;
                    serializer.Serialize(jw, BuildJsonTreeCollection(collection));
                }
            }
        }

        public JsonTreeCollection BuildJsonTreeCollection(TreeCollection collection)
        {
            JsonTreeCollection jsonTreeCollection = new JsonTreeCollection();

            foreach (Tree tree in collection.Collection)
            {
                jsonTreeCollection.Collection.Add(BuildJsonTreeObject(tree));
            }

            return jsonTreeCollection;
        }
        
        public JsonTree BuildJsonTreeObject(Tree tree)
        {
            JsonTree jsonTree = new JsonTree();

            jsonTree.ID = tree.ID;

            foreach (TreeItem item in tree.Items)
            {
                jsonTree.Items.Add(BuildJsonTreeItemObject(item));
            }

            return jsonTree;
        }

        public JsonTreeItem BuildJsonTreeItemObject(TreeItem item)
        {
            JsonTreeItem jsonTreeItem = new JsonTreeItem();
            jsonTreeItem.ItemID = item.ItemID;
            jsonTreeItem.ItemType = item.ItemType;
            jsonTreeItem.Mode = item.Mode;
            jsonTreeItem.Name = item.Name;
            return jsonTreeItem;
        }

        public void BuildJsonActivityCollectionFile(Stream stream, ActivityCollection collection)
        {
            using (StreamWriter sw = new StreamWriter(stream))
            {
                using (JsonWriter jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = Formatting.Indented;

                    JsonSerializer serializer = new JsonSerializer();

                    serializer.NullValueHandling = NullValueHandling.Ignore;
                    serializer.Serialize(jw, BuildJsonActivityCollection(collection));
                }
            }
        }

        public JsonActivityCollection BuildJsonActivityCollection(ActivityCollection collection)
        {
            JsonActivityCollection jsonCollection = new JsonActivityCollection();
            List<Activity> list = new List<Activity>();

            // Filter out groups from the collection
            foreach (ActivityObjectBase activity in collection.Items)
            {
                if (activity is Activity)
                {
                    list.Add(activity as Activity);
                }
            }

            list.Sort(new ActivityComparer(true, "Name"));

            foreach (Activity item in list)
            {
                jsonCollection.Collection.Add(BuildJsonActivityObject(item));
            }

            return jsonCollection;
        }

        public JsonActivityBase BuildJsonActivityObject(Activity activity)
        {
            JsonActivityBase jsonActivity = new JsonActivityBase();
            jsonActivity.ID = activity.ID;
            jsonActivity.Name = activity.Name;
            jsonActivity.ShortName = activity.ShortName;
            jsonActivity.Description = activity.Description;

            return jsonActivity;
        }
    }
}

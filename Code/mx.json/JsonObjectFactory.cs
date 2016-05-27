using mx.core;
using mx.core.Utility;
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

        public void BuildJsonTreeFile(Stream stream, Tree tree)
        {
            using (StreamWriter sw = new NoCloseStreamWriter(stream))
            {
                using (JsonWriter jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = Formatting.Indented;

                    JsonSerializer serializer = new JsonSerializer();

                    serializer.NullValueHandling = NullValueHandling.Ignore;
                    serializer.Serialize(jw, BuildJsonTreeObject(tree));
                    sw.Flush();
                }
            }
        }

        public JsonTree BuildJsonTreeObject(Tree tree)
        {
            JsonTree jsonTree = new JsonTree();

            //jsonTree.ID = tree.ID;

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

        public void BuildJsonActivityFile(StreamWriter sw, Activity activity)
        {
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                jw.Formatting = Formatting.Indented;

                JsonSerializer serializer = new JsonSerializer();

                serializer.NullValueHandling = NullValueHandling.Ignore;
                serializer.Serialize(jw, BuildJsonActivityObject(activity));
                sw.Flush();
            }
        }

        public void BuildJsonGroupFile(StreamWriter sw, Group group)
        {
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                jw.Formatting = Formatting.Indented;

                JsonSerializer serializer = new JsonSerializer();

                serializer.NullValueHandling = NullValueHandling.Ignore;
                serializer.Serialize(jw, BuildJsonGroupObject(group));
                sw.Flush();
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
            jsonActivity.Name = activity.Name;
            jsonActivity.ShortName = activity.ShortName;
            jsonActivity.Description = activity.Description;
            jsonActivity.UniqueID = activity.ID;

            foreach (TrackingEntry entry in activity.TimeEntries.Collection)
            {
                jsonActivity.TimeEntries.Add(BuildJsonTrackingEntry(entry));
            }

            return jsonActivity;
        }

        public JsonGroup BuildJsonGroupObject(Group group)
        {
            JsonGroup jsonGroup = new JsonGroup();
            jsonGroup.UniqueID = group.ID;
            jsonGroup.GroupCode = group.GroupCode;

            return jsonGroup;
        }

        public JsonScheduleItem BuildJsonScheduleItem(ScheduleItem item)
        {
            JsonScheduleItem jsonItem = new JsonScheduleItem();
            jsonItem.ActivityID = item.ActivityID;
            jsonItem.Status = item.Status;

            return jsonItem;
        }

        public JsonScheduleBlock BuildJsonScheduleBlock(ScheduleBlock block)
        {
            JsonScheduleBlock jsonBlock = new JsonScheduleBlock();
            List<Activity> list = new List<Activity>();

            jsonBlock.Name = block.Name;
            jsonBlock.Description = block.Description;

            // Filter out groups from the collection
            foreach (ScheduleItem item in block.Collection)
            {
                jsonBlock.Items.Add(BuildJsonScheduleItem(item));
            }

            return jsonBlock;
        }

        public JsonTrackingEntry BuildJsonTrackingEntry(TrackingEntry entry)
        {
            JsonTrackingEntry jsonEntry = new JsonTrackingEntry();
            jsonEntry.Charger = entry.Charger.ID;
            jsonEntry.Date = entry.Date.ToString("O");
            jsonEntry.Duration = entry.Hours.ToString("0.00");

            return jsonEntry;
        }

        public JsonTrackingCollection BuildJsonTrackingCollection(TrackingCollection collection)
        {
            JsonTrackingCollection jsonCollection = new JsonTrackingCollection();

            foreach (TrackingEntry entry in collection.Collection)
            {
                jsonCollection.Collection.Add(BuildJsonTrackingEntry(entry));
            }

            return jsonCollection;
        }

        public void LoadActivityFileProperties(Activity activity, Stream stream, UserCollection userCollection)
        {
            using (StreamReader sr = new StreamReader(stream))
            {
                JsonSerializer serializer = new JsonSerializer();
                JsonActivityBase jsonActivity = (JsonActivityBase)serializer.Deserialize(sr, typeof(JsonActivityBase));

                activity.Name = jsonActivity.Name;
                activity.ShortName = jsonActivity.ShortName;
                activity.Description = jsonActivity.Description;
                activity.ID = jsonActivity.UniqueID;

                // TODO: Option needed for only name and not additional properties?
                foreach (JsonTrackingEntry entry in jsonActivity.TimeEntries)
                {
                    TrackingEntry trackingEntry = new TrackingEntry();
                    trackingEntry.Charger = userCollection.LookupOrAdd(entry.Charger);
                    trackingEntry.Date = DateTime.Parse(entry.Date);
                    trackingEntry.Hours = double.Parse(entry.Duration);

                    activity.TimeEntries.Add(trackingEntry);
                }
            }
        }

        public void LoadGroupFileProperties(Group group, Stream stream)
        {
            // deserialize JSON directly from a file
            using (StreamReader sr = new StreamReader(stream))
            {
                JsonSerializer serializer = new JsonSerializer();
                JsonGroup jsonGroup = (JsonGroup)serializer.Deserialize(sr, typeof(JsonGroup));

                group.GroupCode = jsonGroup.GroupCode;
                group.ID = jsonGroup.UniqueID;
            }
        }
    }
}

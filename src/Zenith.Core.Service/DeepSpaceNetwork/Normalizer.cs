using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Serialization;
using Zenith.Core.Models.DeepSpaceNetwork;
using Zenith.Core.Models.DeepSpaceNetwork.XML;

namespace Zenith.Core.Service.DeepSpaceNetwork
{
    internal static class Normalizer
    {
        internal static void Normalize(ref DSNStatus status, DSNConfigResult configResult)
        {
            foreach (var site in configResult.Sites.Sites)
            {
                var newSite = new Site
                {
                    Identifier = site.Name,
                    Name = site.FriendlyName,
                    Coordinates = null
                };

                status.Sites.Add(newSite);

                foreach (var dish in site.Dishes)
                {
                    var newDish = new Dish
                    {
                        Identifier = dish.Name,
                        Name = dish.FriendlyName,
                        Type = dish.DishType
                    };

                    status.Dishes.Add(newDish);
                }
            }

            foreach (var spacecraft in configResult.SpacecraftMap.Spacecrafts)
            {
                var newSpacecraft = new Spacecraft
                {
                    Identifier = spacecraft.Name,
                    ExplorerName = spacecraft.ExplorerName,
                    Name = spacecraft.FriendlyName
                };

                status.Spacecrafts.Add(newSpacecraft);
            }

            status.Dishes.Sort((a, b) => a.Name.CompareTo(b.Name));
            status.Sites.Sort((a, b) => a.Name.CompareTo(b.Name));
            status.Spacecrafts.Sort((a, b) => a.Name.CompareTo(b.Name));
        }

        internal static void Normalize(ref DSNStatus status, DSNQueryResult queryResult)
        {
            /*
            foreach (var station in queryResult.Stations)
            {
                if (Sites.Any(s => s.ID == station.Name))
                {
                    var updateSite = Sites.First(s => s.ID == station.Name);
                    updateSite.TimezoneOffsetMinutes = String.IsNullOrEmpty(station.TimeZoneOffset) ? 0 : (int.Parse(station.TimeZoneOffset) / 1000) / 60;
                    updateSite.TimeReportedUTC = String.IsNullOrEmpty(station.TimeUTC) ? (new DateTime()) : (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds(double.Parse(station.TimeUTC));
                }
            }

            foreach (var dish in queryResult.Dishes)
            {
                foreach (var target in dish.Targets)
                {
                    if (Spacecrafts.Any(s => s.ID.ToUpper() == target.Name.ToUpper()))
                    {
                        var updateSpacecraft = Spacecrafts.First(s => s.ID.ToUpper() == target.Name.ToUpper());
                        updateSpacecraft.UplegRange = string.IsNullOrEmpty(target.UplegRange) ? 0 : double.Parse(target.UplegRange);
                        updateSpacecraft.DownlegRange = string.IsNullOrEmpty(target.DownlegRange) ? 0 : double.Parse(target.DownlegRange);
                        updateSpacecraft.RTLT = string.IsNullOrEmpty(target.RoundTripLightTime) ? 0 : double.Parse(target.RoundTripLightTime);
                    }
                }

                if (Dishes.Any(d => d.ID == dish.Name))
                {
                    var updateDish = Dishes.First(d => d.ID == dish.Name);
                    updateDish.AzimuthAngle = string.IsNullOrEmpty(dish.AzimuthAngle) ? 0 : decimal.Parse(dish.AzimuthAngle);
                    updateDish.ElevationAngle = string.IsNullOrEmpty(dish.ElevationAngle) ? 0 : decimal.Parse(dish.ElevationAngle);
                    updateDish.WindSpeed = string.IsNullOrEmpty(dish.WindSpeed) ? 0 : decimal.Parse(dish.WindSpeed);
                    updateDish.IsMSPA = (dish.IsMSPA == "true");
                    updateDish.IsArray = (dish.IsArray == "true");
                    updateDish.IsDDOR = (dish.IsDDOR == "true");
                    updateDish.Created = string.IsNullOrEmpty(dish.Created) ? new DateTime() : DateTime.Parse(dish.Created);
                    updateDish.Updated = string.IsNullOrEmpty(dish.Updated) ? new DateTime() : DateTime.Parse(dish.Updated);
                    updateDish.Signals = new List<Signal>();

                    foreach (var downSignal in dish.DownSignals)
                    {
                        if (!string.IsNullOrEmpty(downSignal.SignalType) && downSignal.SignalType != "none")
                        {
                            var newSignal = new Signal();
                            newSignal.Direction = "Down";
                            newSignal.Type = downSignal.SignalType;
                            newSignal.TypeDebug = downSignal.SignalTypeDebug;
                            newSignal.DataRate = string.IsNullOrEmpty(downSignal.DataRate) ? 0 : double.Parse(downSignal.DataRate);
                            newSignal.Frequency = string.IsNullOrEmpty(downSignal.Frequency) ? 0 : double.Parse(downSignal.Frequency);
                            newSignal.Power = string.IsNullOrEmpty(downSignal.Power) ? 0 : double.Parse(downSignal.Power);
                            newSignal.Spacecraft = Spacecrafts.FirstOrDefault(s => s.ID.ToUpper() == downSignal.Spacecraft.ToUpper());
                            updateDish.Signals.Add(newSignal);
                        }
                    }

                    foreach (var upSignal in dish.UpSignals)
                    {
                        if (!string.IsNullOrEmpty(upSignal.SignalType) && upSignal.SignalType != "none")
                        {
                            var newSignal = new Signal();
                            newSignal.Direction = "Up";
                            newSignal.Type = upSignal.SignalType;
                            newSignal.TypeDebug = upSignal.SignalTypeDebug;
                            newSignal.DataRate = string.IsNullOrEmpty(upSignal.DataRate) ? 0 : double.Parse(upSignal.DataRate);
                            newSignal.Frequency = string.IsNullOrEmpty(upSignal.Frequency) ? 0 : double.Parse(upSignal.Frequency);
                            newSignal.Power = string.IsNullOrEmpty(upSignal.Power) ? 0 : double.Parse(upSignal.Power);
                            newSignal.Spacecraft = Spacecrafts.FirstOrDefault(s => s.ID.ToUpper() == upSignal.Spacecraft.ToUpper());
                            updateDish.Signals.Add(newSignal);
                        }
                    }

                    updateDish.Targets = new List<Spacecraft>();
                    if (updateDish.Signals.Count > 0)
                    {
                        foreach (var target in dish.Targets)
                        {
                            if (Spacecrafts.Any(s => s.ID.ToUpper() == target.Name.ToUpper()))
                            {
                                updateDish.Targets.Add(Spacecrafts.First(s => s.ID.ToUpper() == target.Name.ToUpper()));
                            }
                        }
                    }
                }
            }

            var Updated = string.IsNullOrEmpty(queryResult.Timestamp) ? (new DateTime()) : (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds(double.Parse(queryResult.Timestamp));

            return new DSNStatusResult
            {
                Spacecrafts = Spacecrafts,
                Sites = Sites,
                Dishes = Dishes,
                LastUpdated = Updated
            };
            */
        }
    }
}

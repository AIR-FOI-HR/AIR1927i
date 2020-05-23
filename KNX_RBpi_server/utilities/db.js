// Require nedb and create a Datastore object
var Datastore  	= require('nedb');
var knx = require('../knx_eibd').knx_event;
// var ReadFromBus = require('knx_eibd').ReadFromBus;

//#region Database loading
var currentDir = process.cwd();

if (currentDir == "/home/pi/knx"){
  //develop
  var objects_db = new Datastore({ filename: 'knx_objects.db', autoload: true });
  objects_db.persistence.setAutocompactionInterval(1800000);

  var rooms_db = new Datastore({ filename: 'knx_rooms.db', autoload: true });
  rooms_db.persistence.setAutocompactionInterval(1800000);

  var object_type_db = new Datastore({ filename: 'object_type.db', autoload: true });
  object_type_db.persistence.setAutocompactionInterval(1800000);

  var scene_db = new Datastore({ filename: 'scene.db', autoload: true });
  scene_db.persistence.setAutocompactionInterval(1800000);

  var scene_object_db = new Datastore({ filename: 'secene_object.db', autoload: true });
  scene_object_db.persistence.setAutocompactionInterval(1800000);

  var central_functions_db = new Datastore({ filename: 'central_functions.db', autoload: true });
  central_functions_db.persistence.setAutocompactionInterval(1800000); 
}else{
  //production
  var objects_db = new Datastore({ filename: 'home/pi/knx/knx_objects.db', autoload: true });
  objects_db.persistence.setAutocompactionInterval(1800000);
  
  var rooms_db = new Datastore({ filename: 'home/pi/knx/knx_rooms.db', autoload: true });
  rooms_db.persistence.setAutocompactionInterval(1800000); 
  
  var object_type_db = new Datastore({ filename: 'home/pi/knx/object_type.db', autoload: true });
  object_type_db.persistence.setAutocompactionInterval(1800000); 
  
  var scene_db = new Datastore({ filename: 'home/pi/knx/scene.db', autoload: true });
  scene_db.persistence.setAutocompactionInterval(1800000); 
  
  var scene_object_db = new Datastore({ filename: 'home/pi/knx/secene_object.db', autoload: true });
  scene_object_db.persistence.setAutocompactionInterval(1800000);
  
  var central_functions_db = new Datastore({ filename: 'home/pi/knx/central_functions.db', autoload: true });
  central_functions_db.persistence.setAutocompactionInterval(1800000); 
  
}
//#endregion

knx.on('bus_event', function (data) { 
  var destAddress = data.destination;
  var dataVal = data.value;
  
  rooms_db.find({ addresses: destAddress }, function (err, rooms) {
    if (rooms[0]){
      var knx_object = {
        address: destAddress,
        room: rooms[0].room_name,
        value: dataVal
      };
      
      objects_db.update({ address: knx_object.address }, knx_object, { upsert: true }, function (err, numReplaced, upsert) {
      });
    }
  });
});

exports.objects_db = objects_db;
exports.rooms_db = rooms_db;
exports.object_type_db = object_type_db;
exports.scene_db = scene_db;
exports.scene_object_db = scene_object_db;
exports.central_functions_db = central_functions_db;
 
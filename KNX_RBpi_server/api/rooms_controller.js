'use strict';

var rooms_db = require('../utilities/db').rooms_db;

//room_addRoom
exports.addRoom = function (req, res) {
	if (req.body.data) {
		var data = req.body.data;
		rooms_db.insert(data, (err, newDoc)=>{
			if(!err)
				res.end('success');	
			else
				res.end('error');	
		});
	}
	else
		res.end('error');	
}

//room_updateRoom
exports.updateRoom = function(req,res){
	if (req.body.data) {
		var data = req.body.data;

		rooms_db.update({ _id: data._id }, data, {}, function (err, numReplaced) {
			if (!err)
				res.end('success');
			else
				res.end('error');	
		});
	}
	else
		res.end('error');	
}

//room_deleteRoom
exports.deleteRoom = function(req,res){
	rooms_db.remove({ _id: req.body.id }, {}, function (err, numRemoved) {
		res.end('success');
	});
}

//room_getAll
exports.getAll = function (req, res) {
	rooms_db.find({}, function (err, docs) {
		res.json(docs);
		if (!err)
			res.end('success');
		else
			res.end('error');	
	});
}

//room_get
exports.get = function (req, res) {
if (req.params.room)
	objects_db.find({ room: req.params.room }, function (err, docs) {
		if (!err){
			res.json(docs);
			res.end('success');
		}
		else
			res.end('error');	
	})
}

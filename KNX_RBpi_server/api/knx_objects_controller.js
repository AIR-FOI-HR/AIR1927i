'use strict';

var knx_object_db = require('../utilities/db').objects_db;

//knxObject_addKnxObject
exports.addKnxObject = function (req, res) {
	if (req.body.data) {
		var data = req.body.data;
		knx_object_db.insert(data, (err, newDoc)=>{
			if(!err)
				res.end('success');	
			else
				res.end('error');	
		});
	}
	else
		res.end('error');	
}

//knxObject_updateKnxObject
exports.updateKnxObject = function(req,res){
	if (req.body.data) {
		var data = req.body.data;

		knx_object_db.update({ _id: data._id }, data, {}, function (err, numReplaced) {
			if (!err)
				res.end('success');
			else
				res.end('error');	
		});
	}
	else
		res.end('error');	
}

//knxObject_deleteKnxObject
exports.deleteKnxObject = function(req,res){
	knx_object_db.remove({ _id: req.body.id }, {}, function (err, numRemoved) {
		res.end('success');
	});
}

//knxObject_getAll
exports.getAll = function (req, res) {
	knx_object_db.find({}, function (err, docs) {
		res.json(docs);
		if (!err)
			res.end('success');
		else
			res.end('error');	
	});
}

//knxObject_getAllByRoom
exports.getAllByRoom = function (req, res) {
	knx_object_db.find({ "Room._id": req.params.roomId}, function (err, docs) {
		res.json(docs);
		if (!err)
			res.end('success');
		else
			res.end('error');	
	});
}

//knxObject_get
exports.get = function (req, res) {
if (req.params.knxObject)
	objects_db.find({ knxObject: req.params.knxObject }, function (err, docs) {
		if (!err){
			res.json(docs);
			res.end('success');
		}
		else
			res.end('error');	
	})
}

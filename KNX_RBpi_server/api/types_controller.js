'use strict';

var object_type_db = require('../utilities/db').object_type_db;

//type_addType
exports.addType = function (req, res) {
	if (req.body.data) {
		var data = req.body.data;
		object_type_db.insert(data, (err, newDoc)=>{
			if(!err)
				res.end('success');	
			else
				res.end('error');	
		});
	}
	else
		res.end('error');	
}

//type_updateType
exports.updateType = function(req,res){
	if (req.body.data) {
		var data = req.body.data;

		object_type_db.update({ _id: data._id }, data, {}, function (err, numReplaced) {
			if (!err)
				res.end('success');
			else
				res.end('error');	
		});
	}
	else
		res.end('error');	
}

//type_deleteType
exports.deleteType = function(req,res){
	object_type_db.remove({ _id: req.body.id }, {}, function (err, numRemoved) {
		res.end('success');
	});
}

//type_getAll
exports.getAll = function (req, res) {
	object_type_db.find({}, function (err, docs) {
		res.json(docs);
		if (!err)
			res.end('success');
		else
			res.end('error');	
	});
}

//type_get
exports.get = function (req, res) {
if (req.params.type)
	objects_db.find({ type: req.params.type }, function (err, docs) {
		if (!err){
			res.json(docs);
			res.end('success');
		}
		else
			res.end('error');	
	})
}

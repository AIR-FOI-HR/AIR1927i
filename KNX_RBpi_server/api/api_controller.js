'use strict';

var knx = require('../knx_eibd').knx_event;
var WriteToBus = require('../knx_eibd').WriteToBus;

var objects_db = require('../utilities/db').objects_db;
var rooms_db = require('../utilities/db').rooms_db;

exports.off = function (req, res) {
	WriteToBus("1/1/1", "DPT1", 0); 
	res.end('finished');
};

exports.on = function (req, res) {
	WriteToBus("1/1/1", "DPT1", 1); 
	res.end('finished');
};

exports.multiple = function(req, res){
	if(req.body.data){
		var data = JSON.parse(req.body.data);
		for (let i = 0; i < data.length; i++) {
			const item = data[i];
			WriteToBus(item.Address,"DPT1", item.Value);
		}
	res.end('finished');
	}
}

exports.single = function (req, res) {
	if (req.body.data) {
		var data = req.body.data;
		
		WriteToBus(data.Address, "DPT1", data.Value);
	
		res.end('finished');
	}
}

exports.getRoomsData = function (req, res) {
	if(req.params.room)
		objects_db.find({ room: req.params.room }, function(err, docs){
			res.json(docs);
			res.end();
		})
}

exports.getRooms = function(req, res){
	rooms_db.find({}, function (err, docs) {
		res.json(docs);
		res.end();
	});
}

exports.dim = function(req, res){
	if (req.body.data) {
		var data = req.body.data;
		WriteToBus(data.Address, "DPT5", data.Value);

		res.end('finished');		
	}
	res.end('finished');	
}

//novo

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
}

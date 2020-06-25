'use strict';

var WriteToBus = require('../knx_eibd').WriteToBus;

exports.move = function(req, res){
	if (req.body.data) {
		var data = req.body.data;
		WriteToBus(data.Address, data.DPT, data.Value);

		res.end('finished');		
	}
	res.end('finished');	
}

exports.rotate = function (req, res) {
	if (req.body.data) {
		var data = req.body.data;
		WriteToBus(data.Address, data.DPT, data.Value);

		res.end('finished');
	}
	res.end('finished');
}

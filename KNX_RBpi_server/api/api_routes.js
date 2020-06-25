'use strict';

module.exports = function(app) {
  var types_controller = require('../api/types_controller');
  var rooms_controller = require('../api/rooms_controller');
  var knx_objects_controller = require('../api/knx_objects_controller');
  var lights_controller = require('../api/lights_controller');
  var blinds_controller = require('../api/blinds_controller');

  // Types
  app.route('/api/knx/add-type')
    .post(types_controller.addType)

  app.route('/api/knx/get-types')
    .get(types_controller.getAll);

  app.route('/api/knx/delete-type')
    .post(types_controller.deleteType)

  app.route('/api/knx/update-type')
    .post(types_controller.updateType)

  // Rooms
  app.route('/api/knx/add-room')
    .post(rooms_controller.addRoom)

  app.route('/api/knx/get-rooms')
    .get(rooms_controller.getAll);

  app.route('/api/knx/delete-room')
    .post(rooms_controller.deleteRoom)

  app.route('/api/knx/update-room')
    .post(rooms_controller.updateRoom)

  // Knx objects
  app.route('/api/knx/add-knx-object')
    .post(knx_objects_controller.addKnxObject)

  app.route('/api/knx/get-knx-objects')
    .get(knx_objects_controller.getAll);

  app.route('/api/knx/delete-knx-object')
    .post(knx_objects_controller.deleteKnxObject)

  app.route('/api/knx/update-knx-object')
    .post(knx_objects_controller.updateKnxObject)

  app.route('/api/knx/get-knx-objects/:roomId')
    .get(knx_objects_controller.getAllByRoom);

    // Lights
  app.route('/api/knx/dim')
    .post(lights_controller.dim)

  app.route('/api/knx/switch')
    .post(lights_controller.switch)

  // Blinds
  app.route('/api/knx/rotate')
    .post(blinds_controller.rotate)

  app.route('/api/knx/move')
    .post(blinds_controller.move)
};



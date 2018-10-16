from flask import Flask
from flask import abort
from flask import request
from flask import make_response
from flask import jsonify
from db_loader import DBLoader


app = Flask(__name__)


@app.route('/api/add', methods=['POST'])
def add_meal():
    if not request.json:
        abort(400)

    for elem in ['name', 'price']:
        if elem not in request.json:
            return make_response(jsonify({'error': 'missing argument {}'.format(elem)}), 400)

    loader = DBLoader()
    loader.add_meal(request.json['name'], float(request.json['price']))
    del(loader)

    return jsonify({'success': 'new record was added'}, 201)


if __name__ == '__main__':
    app.run(debug=True)

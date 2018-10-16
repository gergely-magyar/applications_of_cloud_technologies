import random


possibleGenders = ['Male', 'Female']
possibleAge = range(18, 48)
possibleRegion = ['west', 'center', 'east']
possibleHometown = ['village', 'city']
possibleEducation = ['none', 'elementary', 'high', 'bachelor', 'master',
                     'doctorate']
possibleHasHouse = [True, False]
possibleYearsWorked = range(0, 30)
degreeToYears = {
    'none': 18,
    'elementary': 18,
    'high': 18,
    'bachelor': 21,
    'master': 23,
    'doctorate': 27
}


def open_file(path):
    return open(path, 'w')


def write_line(writeToFile, lineList):
    print(lineList)
    writeToFile.write(','.join(lineList))
    writeToFile.write('\n')


def calculate_endclass(gender, age, region, hometown,
                       education, hasHouse, yearsWorked):
    base = 600
    if region == 'east':
        base -= 50
    elif region == 'west':
        base += 100

    if hometown == 'village':
        base -= 50
    else:
        base += 50

    rate = 0.01
    if education == 'bachelor':
        base += 50
        rate = 0.05
    elif education == 'master':
        base += 100
        rate = 0.1
    elif education == 'doctorate':
        base += 120
        rate = 0.15

    final = base * ((1 + rate) ** yearsWorked)
    return final


def generate_data():
    gender = random.choice(possibleGenders)
    age = random.choice(possibleAge)
    region = random.choice(possibleRegion)
    hometown = random.choice(possibleHometown)
    if age < 21:
        education = random.choice(possibleEducation[:3])
    elif age < 23:
        education = random.choice(possibleEducation[:4])
    elif age < 27:
        education = random.choice(possibleEducation[:5])
    else:
        education = random.choice(possibleEducation)
    hasHouse = random.choice(possibleHasHouse)
    startedWorking = degreeToYears[education]
    yearsWorked = random.randint(0, age - startedWorking)

    category = calculate_endclass(gender, age, region, hometown,
                                  education, hasHouse, yearsWorked)

    return [gender, str(age), region, hometown,
            education, str(hasHouse), str(yearsWorked), str(category)]


myFile = open_file('test.csv')
write_line(myFile, ['gender', 'age', 'region', 'hometown',
                    'education', 'hashouse', 'yearsworked', 'salary'])
for x in range(10000):
    write_line(myFile, generate_data())

import cv2
import MySQLdb
import os

def Connect_Mysql():
    db = MySQLdb.connect("127.0.0.1", "root", "root", "internetbar_db")
    cursor = db.cursor()
    cursor.execute("select count(*) from face_info")
    data = cursor.fetchone()
    
    #print "yigong you : %s " % data
    a = str(data[0])
    #print data, type(a), a
    db.close()
    return a

def select_face():
    db = MySQLdb.connect("127.0.0.1", "root", "root", "internetbar_db")
    cursor = db.cursor()
    cursor.execute("select user_id from user_info where user_id not in (select user_id from face_info)")
    data = cursor.fetchone()
    
    b = str(data[0])
    db.close()
    return b

def add_face(a, b):

    db = MySQLdb.connect("127.0.0.1", "root", "root", "internetbar_db")
    cursor = db.cursor()
    sql = "insert into face_info (user_id, face_id) values(%(a)s, %(b)s)"
    value = {"a": a, "b": b}
    cursor.execute(sql, value)
    db.commit()
    db.close()
	
    
def generate(a):
    face_cascade = cv2.CascadeClassifier('./cascades/haarcascade_frontalface_default.xml')
    eye_cascade = cv2.CascadeClassifier('./cascades/haarcascade_eye.xml')
    camera = cv2.VideoCapture(0)
    count = 0
    if (os.path.exists(r'./data/%s' % a) == False):
        os.makedirs(r'./data/%s' % a)
        
    while (True):
        ret, frame = camera.read()
        gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
    
        faces = face_cascade.detectMultiScale(gray, 1.3, 5)
        
        for (x,y,w,h) in faces:
            img = cv2.rectangle(frame,(x,y),(x+w,y+h),(255,0,0),2)
            
            f = cv2.resize(gray[y:y+h, x:x+h], (200, 200))
            cv2.imwrite('./data/%s/%s.pgm' % (str(a), str(count)) , f)
            count += 1
            
        cv2.imshow("camera", frame)
        if cv2.waitKey(1000 / 12) & (count == 25):
            break
        
    camera.release()
    cv2.destroyAllWindows()
    
if __name__ == "__main__":
    a = Connect_Mysql()
    generate(a)
    #print type(a)
    b = select_face()
    add_face(b, a)
	
	
    
    
    
    
    
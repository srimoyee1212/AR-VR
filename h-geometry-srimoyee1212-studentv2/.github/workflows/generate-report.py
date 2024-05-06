#! /usr/bin/python3
from operator import index
import subprocess
import json
import sys
import os
from subprocess import DEVNULL

# TODO: many things below
# Score
# setup % complete
# add total tests

student_assignment = 'geometry'
totalAvailablePoints = 100


try:
  arg = sys.argv[1]
  try:
    totalAvailablePoints = int(arg)
  except ValueError as e:
    totalAvailablePoints = 100
except IndexError as i:
  totalAvailablePoints = 100

totalTestsWritten = 16
try:
  arg = sys.argv[2]
  try:
    totalTestsWritten = int(arg)
  except ValueError as e:
    totalTestsWritten = 16
except IndexError as i:
  totalTestsWritten = 16

# ... Set up some soft coded variables
s_last = 'lastName'
s_first = 'firstName'
s_netid = 'ID'
s_github = 'Username'
s_repourl = 'repoURL'
s_videourl = 'videoURL'
s_apkurl = 'apkURL'
s_writeup = 'workSummary'

s_totalTestFiles = '0'
numberOfTests = 0

totalTestsPassed = 0
totalTestsFailed = 0
totalTestsSkipped = 0
totalTestsInconclusive = 0

data = []
testNames = []
resultsSummaryList = []

def processTestJSON():
  # running python script from repo root
  global data
  global numberOfTests
  global s_totalTestFiles
  testResultLocation = 'TestResults/'
  try:
    jsonFileList = os.listdir(testResultLocation)
  except:
    return
  numberOfTests = len(jsonFileList)
  s_totalTestFiles = str(len(jsonFileList))
  print("Numer of Tests: "+s_totalTestFiles)
  for filename in jsonFileList:
    with open(testResultLocation+filename, 'r') as f:
      data.append(json.load(f))


processTestJSON()

for j in data:
  passed = j.get('Passed')
  failed = j.get('Failed')
  skipped = j.get('Skipped')
  inconclusive = j.get('Inconclusive')
  totalTestsPassed += int(passed)
  totalTestsFailed+= int(failed)
  totalTestsSkipped += int(skipped)
  totalTestsInconclusive += int(inconclusive)

# pull the text results
for j in data:
  a = j.get('Tests')
  for test in a:
    b = test.get('Test')
    c = test.get('Result')
    resultsSummaryList.append(b+ ": " +c)

for j in data:
  a = j.get('Class')
  testNames.append(a)

for resultSummary in resultsSummaryList:
  print(resultSummary)
for testName in testNames:
  print(testName)

print(totalTestsPassed)
print(totalTestsFailed)
print(totalTestsSkipped)
print(totalTestsInconclusive)

with open('Assets/Submission/Student Information.asset', 'r') as f:
  studentinfo = f.read()

# ... function to search the file

def get_field(fname, z):
  try:
    print("trying")
    hval = studentinfo.find('m_EditorClassIdentifier:')
    ival = studentinfo.find(fname, hval)
    jval = studentinfo.find(":", ival) + 2
    print(hval)
    print(ival)
    print(jval)
    if ( ival < 0 ):
      return "field not found"
    kval = studentinfo.find('\n', ival)
    if ( z == 1 ):
      pval = studentinfo[jval:kval]
    else:
      pval = studentinfo[jval:]
    return pval 
  except:
    print(f'failed to find {fname}')
    return "field not applicaple"
  

student_firstname = get_field(s_first, 1)
student_lastname = get_field(s_last, 1)
student_netid = get_field(s_netid, 1)
student_gituser = get_field(s_github, 1)
student_repourl = get_field(s_repourl, 1)
student_videourl = get_field(s_videourl, 1)
student_apkurl = get_field(s_apkurl, 1)
student_writeup = get_field(s_writeup, 0)
if student_writeup is None:
  student_writeup = ""

student_fullname = student_firstname + ' ' + student_lastname

texfilename = student_lastname + '_' + student_firstname + '_' + student_netid + '_' + student_assignment + '.tex'
print(texfilename)
texfilename = texfilename.replace(" ","")
texfilename = texfilename.lower()
print(texfilename)

pdffilename = student_lastname + '_' + student_firstname + '_' + student_netid + '_' + student_assignment + '.pdf'
print(pdffilename)
pdffilename = pdffilename.replace(" ","")
pdffilename = pdffilename.lower()
print(pdffilename)

texfilereference = open('./texname', 'w')
texfilereference.write(texfilename)
texfilereference.close()

pdffilereference = open('./pdfname', 'w')
pdffilereference.write(pdffilename)
pdffilereference.close()

if totalTestsWritten == 0:
  scoreFraction = 0
  scorePercentage = 0
else:
  scoreFraction = totalTestsPassed/totalTestsWritten
  scorePercentage = scoreFraction * 100
scorePoints = scoreFraction * totalAvailablePoints


# ... SECTION: BEGIN WRITING OUT TO LATEX
# ... open latex 
latex = open('./'+texfilename, 'w')
latex.write('\\documentclass{article}')
latex.write('\n')
latex.write('\\usepackage{fontspec}')
latex.write('\n')
latex.write('\\setmainfont[Ligatures=TeX]{DejaVu Sans}')
latex.write('\n')
latex.write('\\title{Submission: ' + student_assignment + '}')
latex.write('\n')
latex.write('\\author{' + student_fullname + '}')
latex.write('\n')
latex.write('\\usepackage{titling}')
latex.write('\n')
latex.write('\\usepackage{multicol}')
latex.write('\n')
latex.write('\\usepackage{relsize}')
latex.write('\n')
latex.write('\\usepackage{changepage}')
latex.write('\n')
latex.write('\\date{}')
latex.write('\n')
latex.write('\\usepackage[margin=0.3in]{geometry}')
latex.write('\n')
latex.write('\\PassOptionsToPackage{hyphens}{url}\\usepackage{hyperref}')
latex.write('\n')
latex.write('\\begin{document}')
latex.write('\\begin{multicols}{2}')
latex.write('\n')
latex.write('\\relscale{1.4}')
latex.write('\n')

latex.write('\section*{Student Information}')
# ... add student data (name, netid ...)
latex.write('\n')
latex.write('\\textbf{Name: }' + student_fullname)
latex.write('\n')
latex.write('\\\\')
latex.write('\n')

latex.write('\\textbf{Cornell NetID: }' + student_netid)
latex.write('\n')
latex.write('\\\\')
latex.write('\n')

latex.write('\\textbf{GitHub Username: }' + student_gituser)
latex.write('\n')

latex.write('\\columnbreak')
latex.write('\n')


#############################
#     Test Results Header
#############################
# ... create new section for the tests
latex.write('\\section*{\\hfill\\llap{Unit Test Results}}')
latex.write('\n')
latex.write('\\hfill\\llap{\\textbf{Score (out of ')
latex.write(str(totalAvailablePoints))
latex.write('): ')
latex.write('{:3d}'.format(int(scorePoints))+"}}")
latex.write('\n')

print('{:3d}'.format(int(scorePoints)))

latex.write('\\newline\\rlap{}\\hfill\\llap{Score \\%: ')
latex.write('{:3d}'.format(int(scorePercentage))+"}")
latex.write('\n')

latex.write('\\newline\\rlap{}\\hfill\\llap{Total Tests: ')
latex.write(str(totalTestsWritten)+"}")
latex.write('\n')

latex.write('\\newline\\rlap{}\\hfill\\llap{Total Passed: ')
latex.write(str(totalTestsPassed)+"}")
latex.write('\n')

latex.write('\\newline\\rlap{}\\hfill\\llap{Total Failed: ')
latex.write(str(totalTestsFailed)+"}")
latex.write('\n')

latex.write('\\newline\\rlap{}\\hfill\\llap{Total Skipped: ')
latex.write(str(totalTestsSkipped)+"}")
latex.write('\n')

latex.write('\\newline\\rlap{}\\hfill\\llap{Total Inconclusive: ')
latex.write(str(totalTestsInconclusive)+"}")

latex.write('\\end{multicols}')
latex.write('\n')

#############################
#     Test Results Header
#############################
# ... create new section for the URLs

latex.write('\\relscale{1.0}')
latex.write('\n')
latex.write('\\subsection*{Links}')
latex.write('\n')

latex.write('GitHub Repository Link: \\\\ \\url{' + student_repourl + '}')
latex.write('\n')
latex.write('\\\\ \\\\')
latex.write('\n')

latex.write('Video Recording Link: \\\\ \\url{' + student_videourl + '}')
latex.write('\n')
latex.write('\\\\ \\\\')
latex.write('\n')

latex.write('Quest APK Link: \\\\ \\url{' + student_apkurl + '}')
latex.write('\n')

def cleanupStudentWriteUp(studentWriteup):
  if(len(studentWriteup) == 0):
    cleanWriteup = ""
    return cleanWriteup
  modifiedstr = studentWriteup.rstrip()
  modifiedstr = modifiedstr.replace('\\n','\\\\')
  modifiedstr = modifiedstr.replace('#','\#')
  modifiedstr = modifiedstr.replace('$','\$')
  modifiedstr = modifiedstr.replace('%','\%')
  modifiedstr = modifiedstr.replace('~','\~')
  modifiedstr = modifiedstr.replace('_','\_')
  modifiedstr = modifiedstr.replace('^','\^')
  #modifiedstr = modifiedstr.replace('\\','\\\\')
  modifiedstr = modifiedstr.replace('{','\{')
  cleanWriteup = modifiedstr.replace('}','\}')
  if(len(cleanWriteup) == 0):
    cleanWriteup = ""
    return cleanWriteup
  if (cleanWriteup[0] == "\"" or cleanWriteup[0] =="'"):
    print("yeah it's there")
    cleanWriteup = cleanWriteup[1:];
  
  print(cleanWriteup[-1])
  if (cleanWriteup[-1] == "\"" or cleanWriteup[-1] =="'"):
    print(cleanWriteup[-1]+"yeah it's there")
    cleanWriteup = cleanWriteup[:-2];


  print(cleanWriteup)
  return cleanWriteup

latex.write('\\begin{adjustwidth}{0 in}{0 in}')
latex.write('\n')
latex.write('\\relscale{1.0}')
latex.write('\n')
latex.write('\\subsection*{Work Summary}')
latex.write('\n')
latex.write('\\raggedright')
latex.write('\n')
latex.write(cleanupStudentWriteUp(student_writeup))
latex.write('\n')
latex.write('\\end{adjustwidth}')
latex.write('\\relscale{1.0}')
latex.write('\n')
latex.write('\\newpage')
latex.write('\n')
latex.write('\\noindent ')

latex.write(' \\\\ Test Suites: \\\\')
latex.write('\n')

for testName in testNames:
  latex.write(testName + '\\\\')
  latex.write('\n')

latex.write('Individual Test Results: \\\\')
latex.write('\n')

for resultSummary in resultsSummaryList:
  latex.write(resultSummary + '\\\\')
  latex.write('\n')

# ... finish adding LaTeX boilerplate
latex.write('\end{document}')
latex.write('\n')
# ... needed to have file available for TeX process
print("closing file")
f.close()
# ... SECTION: END WRITING OUT TO LATEX
# ... stdout notice that script is has completed
print("exiting... ")


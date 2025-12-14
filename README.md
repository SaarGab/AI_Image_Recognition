This is an image recognition project that uses the Contrastive Language-Image Pre-training (CLIP) technique for a machine learning model. The model also uses online pictures fetched from the website
https://unsplash.com as a way of comparing the desired image by the user with online images from the web.

Important: Before running the attached code, please do the following:
1. Open the Visual Studio IDE (if you don't have it then please download it).
2. Create a new Windows Forms App project.
3. Copy the entire code from the attached Form1.cs file to your automatically generated Form1.cs file of the project.
4. Copy the entire code from the attached Form1.Designer.cs file to your automatically generated Form1.Designer.cs file of the project.
5. Copy the entire code from the attached Program.cs file to your automatically generated Program.cs file of the project.
6. Go to the website https://unsplash.com, create an account (or log in to your existing account if you have one) and then go to the Bookmarks tab. There, scroll down to the Developers/API option and click it. Then click
on "your apps" --> New application --> Accept all the terms --> Give a name and description to your application (could be anything, just make sure it isn't empty) --> Scroll down to your newly created Access Key --> copy it
--> paste it in the Form1.cs code as the value of the UNSPLASH_ACCESS_KEY string variable. Also, do NOT share you Access key with anyone.
7. Open the Solution explorer of your project and then right click it. After that, click "Manage NuGet Packages for Solution..." and then in the Browse tab of the newly opened window, search and install the 3 following
packages:
1) Microsoft.ML.OnnxRuntime
2) Newtonsoft.Json
3) SixLabors.ImageSharp

Now, run the code by clicking on the debug tab --> Start without Debugging. You will get the following screen:

<img width="572" height="597" alt="image" src="https://github.com/user-attachments/assets/85046f09-9ebc-49cf-9ddf-b441fadf767a" />

Click on the "Browse..." button and input a path to a local folder on your computer that contains images, and images only (it could take a while for the application to load after you input the path to the folder).

For example, I entered a local folder called "phone_pics" that contains many images, and images only:

<img width="1102" height="633" alt="image" src="https://github.com/user-attachments/assets/71f108a9-159d-4687-9d48-5469929e0589" />

The app will then show you how many local images it loaded from the folder

<img width="583" height="601" alt="image" src="https://github.com/user-attachments/assets/0701102f-64c7-4f1f-b1d9-e5de1db7aa76" />

Now, input a search term into the "Enter search term..." textbox. For instance, I entered the term "anime":

<img width="568" height="596" alt="image" src="https://github.com/user-attachments/assets/36c30feb-4a8e-45d3-87ac-fc56558e321a" />

Then, click on the "Search" Button. The app will find the local image in your folder that best fits your search term:

<img width="573" height="597" alt="image" src="https://github.com/user-attachments/assets/2b3b1975-ad87-460b-942f-e113356a953a" />

You can also click on the "Next" button to find the next best matching local image in your folder:

<img width="723" height="766" alt="image" src="https://github.com/user-attachments/assets/7758b2b0-3128-4891-ae7f-ddf24bd10a0d" />

And so forth.

You can also enter a different search term, and then click on "Search" to find the local image from your folder that best matches the new search term you entered:

<img width="577" height="595" alt="image" src="https://github.com/user-attachments/assets/89441cee-1a50-41e8-9bd6-9e2f4fe08047" />

And like before, you can click "Next" to see the next best matching local image from your folder.

Hope you enjoy using the App! :)


































































































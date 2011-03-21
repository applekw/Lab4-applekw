using System;

using Expedia;
using Rhino.Mocks;
using NUnit.Framework;

namespace ExpediaTest
{
	[TestFixture()]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[SetUp()]
		public void SetUp()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[Test()]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}

        [Test()]
        public void TestThatCarGetsCorrectLocation()
        {
            
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            String carLocation1 = "Tallahassee, FL";
            String carLocation2 = "Dallas, TX";

            using (mocks.Record())
            {
                //The mock will return "Tallahassee, FL" When the given carNumber is 4145
                mockDatabase.getCarLocation(4145);
                LastCall.Return(carLocation1);

                //The mock will return "Dallas, TX" when the given carNumber is 9000
                mockDatabase.getCarLocation(9000);
                LastCall.Return(carLocation2);
            }

            var target = ObjectMother.Saab();

            target.Database = mockDatabase;
            String result = target.getCarLocation(4145);
            Assert.AreEqual(result, carLocation1);

            result = target.getCarLocation(9000);
            Assert.AreEqual(result, carLocation2);
        }

        [Test()]
        public void TestThatMileageISCorrect()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            Int32 Miles = 100;
       

            mockDatabase.Miles = Miles;

            var target = ObjectMother.BMW();
            target.Database = mockDatabase;

            int mileage = target.Mileage;
            Assert.AreEqual(mileage, Miles);

        }

		
		[Test()]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}
	}
}
